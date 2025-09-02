using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories
{
    public  class PropertyRepository : BaseRepository<PropertyAggregate, Guid>, IPropertyRepository
    {
        public PropertyRepository(MillionRealStateDbContext context) : base(context)
        {
        }
    }
}
