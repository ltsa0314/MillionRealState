using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.PropertyTrace
{
    public interface IPropertyTraceRepository : IRepository<PropertyTraceAggregate, Guid>
    {
    }
}
