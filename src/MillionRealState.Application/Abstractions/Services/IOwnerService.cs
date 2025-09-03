using MillionRealState.Application.Common.Results;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Abstractions.Services
{
            public interface IOwnerService
    {
        Task<Guid> CreateAsync(CreateOwnerDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid idOwner, UpdateOwnerDto dto, CancellationToken ct = default);
        Task<OwnerDto> GetByIdAsync(Guid idOwner, CancellationToken ct = default);
        Task<PagedResult<OwnerDto>> ListAsync(OwnerFilterDto f, CancellationToken ct = default);
    }
}
