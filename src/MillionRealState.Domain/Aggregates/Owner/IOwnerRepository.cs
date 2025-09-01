using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Owner
{
    internal interface IOwnerRepository : IRepository<OwnerAggregate, Guid>
    {
    }
}
