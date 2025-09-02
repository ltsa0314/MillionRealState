using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories
{
    public  class PropertyTraceRepository : BaseRepository<PropertyTraceAggregate, Guid>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(MillionRealStateDbContext context) : base(context)
        {
        }
    }
}
