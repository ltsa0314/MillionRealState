using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    public class PropertyWriteRepository : BaseWriteRepository<PropertyAggregate, Guid>, IPropertyWriteRepository
    {
        public PropertyWriteRepository(MillionRealStateDbContext context) : base(context)
        {
        }

    }
}
