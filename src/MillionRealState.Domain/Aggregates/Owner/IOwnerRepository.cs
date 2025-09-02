using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public interface IOwnerRepository : IRepository<OwnerAggregate, Guid>
    {
    }
}
