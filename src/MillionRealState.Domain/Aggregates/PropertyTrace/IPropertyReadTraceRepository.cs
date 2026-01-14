using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Domain.Aggregates.PropertyTrace
{
    public interface IPropertyReadTraceRepository : IReadRepository<PropertyTraceAggregate, Guid>
    {
    }
}
