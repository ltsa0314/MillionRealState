using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.SeedWork.Contracts
{
    /// <summary>
    /// Define las operaciones de lectura para un repositorio de agregados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad agregada.</typeparam>
    /// <typeparam name="TKey">Tipo de la clave primaria de la entidad.</typeparam>
    public interface IRepositoryRead<TEntity, TKey>
        where TEntity : AggregateRoot
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>Entidad encontrada o null si no existe.</returns>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Obtiene todas las entidades.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Verifica si existe una entidad con el identificador especificado.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>True si existe, false en caso contrario.</returns>
        Task<bool> ExistsAsync(TKey id);
    }
}
