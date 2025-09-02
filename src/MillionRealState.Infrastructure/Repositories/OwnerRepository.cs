using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories
{
    internal class OwnerRepository : BaseRepository<OwnerAggregate, Guid>, IOwnerRepository
    {
        public OwnerRepository(MillionRealStateDbContext context) : base(context)
        {
        }
    }
}
