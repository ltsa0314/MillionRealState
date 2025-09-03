using Microsoft.EntityFrameworkCore;
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

        public async Task<(List<OwnerAggregate> Items, int Total)> ListPagedAsync(OwnerFilter filter, CancellationToken ct = default)
        {
            var query = _dbSet.AsQueryable();

            // Filtros dinámicos
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(o => o.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.City))
                query = query.Where(o => o.Address.City.Contains(filter.City));

            if (filter.BirthdayFrom.HasValue)
                query = query.Where(o => o.Birthday >= filter.BirthdayFrom.Value);

            if (filter.BirthdayTo.HasValue)
                query = query.Where(o => o.Birthday <= filter.BirthdayTo.Value);

            // Total antes de paginar
            var total = await query.CountAsync(ct);

            // Paginación
            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
