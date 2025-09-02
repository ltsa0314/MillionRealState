using MillionRealState.Application.Features.Properties.Dtos;

namespace MillionRealState.Application.Abstractions.Services
{
    public interface IPropertyService
    {
        public Task<int> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default);
        public Task UpdateAsync(int id, UpdatePropertyDto dto, CancellationToken ct = default);
        public Task ChangePriceAsync(int id, decimal newPrice, CancellationToken ct = default);
        public Task<PropertyDto> GetByIdAsync(int id, CancellationToken ct = default);
        public Task<List<PropertyDto>> GetAllAsync(CancellationToken ct = default);
        public Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
