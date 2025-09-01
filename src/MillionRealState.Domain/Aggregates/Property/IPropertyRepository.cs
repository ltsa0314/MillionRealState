using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Property
{
    internal interface IPropertyRepository : IRepository<PropertyAggregate, Guid>
    {
    }
}
