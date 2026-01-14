using MediatR;
using AutoMapper;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Application.Features.Owner.Commands;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.CommandHandlers
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Guid>
    {
        private readonly IOwnerWriteRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOwnerDto> _validator;

        public CreateOwnerCommandHandler(IOwnerWriteRepository repo, IMapper mapper, IValidator<CreateOwnerDto> validator)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Owner, cancellationToken);

            var entity = _mapper.Map<OwnerAggregate>(request.Owner);
            await _repo.AddAsync(entity);
            return entity.IdOwner;
        }
    }
}