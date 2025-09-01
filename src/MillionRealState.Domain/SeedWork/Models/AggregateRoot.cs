namespace MillionRealState.Domain.SeedWork.Models
{
    /// <summary>
    /// Clase base para entidades que pueden ser ra�z de agregados.
    /// Solo las entidades que hereden de AggregateRoot pueden tener repositorios.
    /// Incluye propiedades comunes como fecha y usuario de creaci�n.
    /// </summary>
    public abstract class AggregateRoot
    {
        /// <summary>
        /// Fecha de creaci�n del registro.
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Usuario que cre� el registro.
        /// </summary>
        public string CreatedBy { get; protected set; } = string.Empty;

        // Puedes agregar m�s propiedades o m�todos comunes si lo necesitas.
    }
}