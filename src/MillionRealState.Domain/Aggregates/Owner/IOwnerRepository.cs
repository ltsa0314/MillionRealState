using MillionRealState.Domain.SeedWork.Contracts;
using MillionRealState.Domain.Aggregates.Owner;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public interface IOwnerRepository : IRepository<OwnerAggregate, Guid>
    {
        /// <summary>
        /// Obtiene una lista paginada de propietarios según el filtro.
        /// </summary>
        /// <param name="filter">Criterios de filtrado y paginación.</param>
        /// <param name="ct">Token de cancelación.</param>
        /// <returns>Tupla con la lista de propietarios y el total de registros.</returns>
        Task<(List<OwnerAggregate> Items, int Total)> ListPagedAsync(OwnerFilter filter, CancellationToken ct = default);
    }
}
