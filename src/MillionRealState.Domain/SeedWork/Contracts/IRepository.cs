using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.SeedWork.Contracts
{
    /// <summary>
    /// Repositorio genérico para agregados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad agregada.</typeparam>
    /// <typeparam name="TKey">Tipo de la clave primaria.</typeparam>
    public interface IRepository<TEntity, TKey> : IReadRepository<TEntity, TKey>, IWriteRepository<TEntity, TKey>
        where TEntity : AggregateRoot
    {
    }
}   
