using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.PropertyTrace
{
    public interface IPropertyWriteTraceRepository : IWriteRepository<PropertyTraceAggregate, Guid>
    {
    }
}
