using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    internal class OwnerReadRepository : BaseReadRepository<OwnerAggregate, Guid>, IOwnerReadRepository
    {
        public OwnerReadRepository(MillionRealStateDbContext context) : base(context)
        {
        }        
    }
}
