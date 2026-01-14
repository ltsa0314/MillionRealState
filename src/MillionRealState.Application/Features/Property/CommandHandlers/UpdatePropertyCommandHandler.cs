using MediatR;
using AutoMapper;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Commands;
using MillionRealState.Application.Features.Property.Dtos;
using MillionRealState.Domain.Aggregates.Owner;

namespace MillionRealState.Application.Features.Property.CommandHandlers
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand>
    {
        private readonly IPropertyReadRepository _readRepo;
        private readonly IPropertyWriteRepository _writeRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePropertyDto> _validator;

        public UpdatePropertyCommandHandler(
            IPropertyReadRepository readRepo,
            IPropertyWriteRepository writeRepo,
            IMapper mapper,
            IValidator<UpdatePropertyDto> validator)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Property, cancellationToken);

            var property = await _readRepo.GetByIdAsync(request.IdProperty);
            if (property == null) throw new KeyNotFoundException($"Property {request.IdProperty} no existe.");

            var addressVO = _mapper.Map<AddressValueObject>(request.Property.Address);

            property.Update(
                request.Property.Name,
                addressVO,
                request.Property.CodeInternal,
                request.Property.Year,
                request.Property.IdOwner
            );

            await _writeRepo.UpdateAsync(property);
        }
    }
}