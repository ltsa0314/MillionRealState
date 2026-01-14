using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Property
{
    public interface IPropertyWriteRepository : IWriteRepository<PropertyAggregate, Guid>
    {       
    }
}
