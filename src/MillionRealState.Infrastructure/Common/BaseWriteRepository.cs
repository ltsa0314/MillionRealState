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
    public abstract class BaseWriteRepository<TEntity, TKey> : IWriteRepository<TEntity, TKey>
        where TEntity : AggregateRoot
    {
        protected readonly MillionRealStateDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseWriteRepository(MillionRealStateDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
                try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Error al guardar la entidad: {innerMessage}", ex);
            }
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró entidad con Id = {id}");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
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
