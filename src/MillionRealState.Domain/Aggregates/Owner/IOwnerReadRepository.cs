using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public interface IOwnerReadRepository : IReadRepository<OwnerAggregate, Guid>
    {
    }
}
