using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public interface IOwnerWriteRepository : IWriteRepository<OwnerAggregate, Guid>
    {
    }
}
