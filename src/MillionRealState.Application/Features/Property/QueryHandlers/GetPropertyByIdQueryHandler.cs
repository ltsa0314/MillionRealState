using MediatR;
using AutoMapper;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Queries;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.QueryHandlers
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
    {
        private readonly IPropertyReadRepository _repo;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyReadRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repo.GetByIdAsync(request.IdProperty);
            return property != null ? _mapper.Map<PropertyDto>(property) : null;
        }
    }
}