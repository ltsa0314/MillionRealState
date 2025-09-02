using Microsoft.EntityFrameworkCore;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Infrastructure.Common;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<PropertyAggregate, Guid>, IPropertyRepository
    {
        public PropertyRepository(MillionRealStateDbContext context) : base(context)
        {
        }

        public async Task<(IReadOnlyList<PropertyAggregate> Items, int TotalCount)> ListPagedAsync(PropertyFilter f, CancellationToken ct = default)
        {
            IQueryable<PropertyAggregate> q = _dbSet.AsNoTracking()
                .Include(p => p.Images);

            // Filtros
            if (f.IdOwner is not null) q = q.Where(p => p.IdOwner == f.IdOwner.Value);
            if (!string.IsNullOrWhiteSpace(f.City)) q = q.Where(p => p.Address.City == f.City);
            if (f.PriceMin is not null) q = q.Where(p => p.Price >= f.PriceMin.Value);
            if (f.PriceMax is not null) q = q.Where(p => p.Price <= f.PriceMax.Value);
            if (f.YearFrom is not null) q = q.Where(p => p.Year >= f.YearFrom.Value);
            if (f.YearTo is not null) q = q.Where(p => p.Year <= f.YearTo.Value);
            if (!string.IsNullOrWhiteSpace(f.Text))
            {
                var t = f.Text.Trim();
                q = q.Where(p =>
                    EF.Functions.Like(p.Name, $"%{t}%") ||
                    EF.Functions.Like(p.CodeInternal, $"%{t}%"))
                    .OrderByDescending(p => p.CreatedAt);
            }

            // Paginación
            var page = Math.Max(1, f.Page);
            var size = Math.Clamp(f.PageSize, 1, 200);

            var total = await q.CountAsync(ct);
            var items = await q
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);

            return (items, total);
        }

    }
}
