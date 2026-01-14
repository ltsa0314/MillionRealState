using MediatR;
using AutoMapper;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Application.Features.Owner.Commands;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.CommandHandlers
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IOwnerReadRepository _readRepo;
        private readonly IOwnerWriteRepository _writeRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateOwnerDto> _validator;

        public UpdateOwnerCommandHandler(
            IOwnerReadRepository readRepo,
            IOwnerWriteRepository writeRepo,
            IMapper mapper,
            IValidator<UpdateOwnerDto> validator)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Owner, cancellationToken);

            var owner = await _readRepo.GetByIdAsync(request.IdOwner);
            if (owner == null) throw new KeyNotFoundException($"Owner {request.IdOwner} no existe.");

            var addressVO = _mapper.Map<AddressValueObject>(request.Owner.Address);
            owner.Update(request.Owner.Name, addressVO, request.Owner.Photo, request.Owner.Birthday);

            await _writeRepo.UpdateAsync(owner);
        }
    }
}