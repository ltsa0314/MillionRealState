using MediatR;
using AutoMapper;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Commands;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.CommandHandlers
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyWriteRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePropertyDto> _validator;

        public CreatePropertyCommandHandler(IPropertyWriteRepository repo, IMapper mapper, IValidator<CreatePropertyDto> validator)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Property, cancellationToken);

            var entity = _mapper.Map<PropertyAggregate>(request.Property);
            await _repo.AddAsync(entity);
            return entity.IdProperty;
        }
    }
}