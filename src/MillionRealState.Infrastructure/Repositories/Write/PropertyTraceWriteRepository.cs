using MillionRealState.Domain.Aggregates.PropertyTrace;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    public  class PropertyTraceWriteRepository : BaseWriteRepository<PropertyTraceAggregate, Guid>, IPropertyWriteTraceRepository
    {
        public PropertyTraceWriteRepository(MillionRealStateDbContext context) : base(context)
        {
        }
    }
}
