using MediatR;
using AutoMapper;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Application.Features.Owner.Queries;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.QueryHandlers
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly IOwnerReadRepository _repo;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IOwnerReadRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _repo.GetByIdAsync(request.IdOwner);
            return owner != null ? _mapper.Map<OwnerDto>(owner) : null;
        }
    }
}