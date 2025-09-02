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
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : AggregateRoot
    {
        protected readonly MillionRealStateDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(MillionRealStateDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró entidad con Id = {id}");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
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

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
