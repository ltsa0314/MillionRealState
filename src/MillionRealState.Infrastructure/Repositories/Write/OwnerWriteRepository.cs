using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories.Write
{
    internal class OwnerWriteRepository : BaseWriteRepository<OwnerAggregate, Guid>, IOwnerWriteRepository
    {
        public OwnerWriteRepository(MillionRealStateDbContext context) : base(context)
        {
        }       
    }
}
