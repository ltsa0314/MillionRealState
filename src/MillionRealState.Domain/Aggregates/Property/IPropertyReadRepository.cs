using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.Property
{
    public interface IPropertyReadRepository : IReadRepository<PropertyAggregate, Guid>
    {        
    }
}
