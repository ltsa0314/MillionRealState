using MediatR;
using AutoMapper;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Application.Features.Owner.Queries;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.QueryHandlers
{
    public class ListOwnersQueryHandler : IRequestHandler<ListOwnersQuery, List<OwnerDto>>
    {
        private readonly IOwnerReadRepository _repo;
        private readonly IMapper _mapper;

        public ListOwnersQueryHandler(IOwnerReadRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<OwnerDto>> Handle(ListOwnersQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<OwnerFilter>(request.Filter);
            var spec = new OwnerByFilterPagedSpecification(filter);
            var (owners, _) = await _repo.GetAllPaginateAsync(spec, cancellationToken);
            return _mapper.Map<List<OwnerDto>>(owners);
        }
    }
}