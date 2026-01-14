using MillionRealState.Application.Common.Results;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Abstractions.Services
{
    public interface IPropertyService
    {
        Task<Guid> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default);
        Task AddImageAsync(Guid idProperty, AddPropertyImageDto dto, CancellationToken ct = default);
        Task ChangePriceAsync(Guid idProperty, ChangePriceDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid idProperty, UpdatePropertyDto dto, CancellationToken ct = default);
        Task<PropertyDto> GetByIdAsync(Guid idProperty, CancellationToken ct = default);
        Task<PagedResult<PropertyDto>> ListAsync(PropertyFilterDto filters, CancellationToken ct = default);
    }
}
