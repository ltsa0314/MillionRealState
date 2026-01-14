using MediatR;
using AutoMapper;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Dtos;
using MillionRealState.Application.Features.Property.Queries;

namespace MillionRealState.Application.Features.Property.QueryHandlers
{
    public class ListPropertiesQueryHandler : IRequestHandler<ListPropertiesQuery, List<PropertyDto>>
    {
        private readonly IPropertyReadRepository _repo;
        private readonly IMapper _mapper;

        public ListPropertiesQueryHandler(IPropertyReadRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PropertyDto>> Handle(ListPropertiesQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<PropertyFilter>(request.Filter);
            var spec = new PropertyByFilterPagedSpecification(filter);
            var (properties, _) = await _repo.GetAllPaginateAsync(spec, cancellationToken);
            return _mapper.Map<List<PropertyDto>>(properties);
        }
    }
}