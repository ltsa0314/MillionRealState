namespace MillionRealState.Domain.SeedWork.Models
{
    /// <summary>
    /// Clase base para entidades que pueden ser raíz de agregados.
    /// Solo las entidades que hereden de AggregateRoot pueden tener repositorios.
    /// Incluye propiedades comunes como fecha y usuario de creación.
    /// </summary>
    public abstract class AggregateRoot
    {
        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Usuario que creó el registro.
        /// </summary>
        public string CreatedBy { get; protected set; } = string.Empty;

        // Puedes agregar más propiedades o métodos comunes si lo necesitas.
    }
}