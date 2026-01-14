using MillionRealState.Domain.Aggregates.PropertyTrace;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    public  class PropertyTraceReadRepository : BaseReadRepository<PropertyTraceAggregate, Guid>, IPropertyReadTraceRepository
    {
        public PropertyTraceReadRepository(MillionRealStateDbContext context) : base(context)
        {
        }
    }
}
