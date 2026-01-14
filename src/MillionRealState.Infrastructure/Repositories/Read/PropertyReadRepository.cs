using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    public class PropertyReadRepository : BaseReadRepository<PropertyAggregate, Guid>, IPropertyReadRepository
    {
        public PropertyReadRepository(MillionRealStateDbContext context) : base(context)
        {
        }

    }
}
