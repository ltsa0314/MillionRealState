using Microsoft.EntityFrameworkCore;
using MillionRealState.Domain.SeedWork.Contracts;
using MillionRealState.Domain.SeedWork.Models;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.Infrastructure.Common
{
    /// <summary>
    /// Repositorio genérico base para entidades de dominio (AggregateRoot).
    /// Implementa operaciones comunes con Entity Framework Core.
    /// Puede ser extendido y sobreescrito por repositorios específicos.
    /// </summary>
    public abstract class BaseReadRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>
        where TEntity : AggregateRoot
    {
        protected readonly MillionRealStateDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseReadRepository(MillionRealStateDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<(List<TEntity> Items, int Total)> GetAllPaginateAsync(IReadSpecification<TEntity> specification, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            // Aplicar filtros (Criteria)
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            // Aplicar Includes
            if (specification.Includes != null)
            {
                foreach (var include in specification.Includes)
                {
                    query = query.Include(include);
                }
            }

            // Ordenamiento
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            // Total antes de paginación
            var total = await query.CountAsync(ct);

            // Paginación
            if (specification.IsPagingEnabled)
            {
                if (specification.Skip.HasValue)
                    query = query.Skip(specification.Skip.Value);
                if (specification.Take.HasValue)
                    query = query.Take(specification.Take.Value);
            }

            var items = await query.ToListAsync(ct);

            return (items, total);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

    }
}
