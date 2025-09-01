using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.SeedWork.Contracts
{
    /// <summary>
    /// Define las operaciones de escritura para un repositorio de agregados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad agregada.</typeparam>
    /// <typeparam name="TKey">Tipo de la clave primaria de la entidad.</typeparam>
    internal interface IRepositoryWrite<TEntity, TKey>
        where TEntity : AggregateRoot
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}