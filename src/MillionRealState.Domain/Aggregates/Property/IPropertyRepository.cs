using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Property
{
    public interface IPropertyRepository : IRepository<PropertyAggregate, Guid>
    {
    }
}
