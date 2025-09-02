using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Property
{
    public interface IPropertyRepository : IRepository<PropertyAggregate, Guid>
    {
        Task<(IReadOnlyList<PropertyAggregate> Items, int TotalCount)> ListPagedAsync(PropertyFilter filter, CancellationToken ct = default);
    }
}
