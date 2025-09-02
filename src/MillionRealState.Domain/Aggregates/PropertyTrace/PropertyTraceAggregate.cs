using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.Aggregates.PropertyTrace
{
    /// <summary>
    /// Agregado ra�z que representa una traza (historial de venta) de una propiedad inmobiliaria.
    /// </summary>
    public class PropertyTraceAggregate : AggregateRoot
    {
        /// <summary>
        /// Identificador �nico de la traza de propiedad.
        /// </summary>
        public int IdPropertyTrace { get; private set; }

        /// <summary>
        /// Fecha de la venta o evento registrado.
        /// </summary>
        public DateTime DateSale { get; private set; }

        /// <summary>
        /// Nombre del evento o comprador.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Valor de la transacci�n.
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Impuesto aplicado en la transacci�n.
        /// </summary>
        public decimal Tax { get; private set; }

        /// <summary>
        /// Identificador de la propiedad asociada.
        /// </summary>
        public int IdProperty { get; private set; }

        /// <summary>
        /// Propiedad de navegaci�n a la propiedad asociada (lazy loading).
        /// </summary>
        public virtual PropertyAggregate Property { get; protected set; } = default!;

        /// <summary>
        /// Constructor principal para crear una traza de propiedad.
        /// </summary>
        /// <param name="dateSale">Fecha de la venta.</param>
        /// <param name="name">Nombre del evento o comprador.</param>
        /// <param name="value">Valor de la transacci�n.</param>
        /// <param name="tax">Impuesto aplicado.</param>
        /// <param name="idProperty">Identificador de la propiedad asociada.</param>
        /// <param name="createdBy">Usuario que crea el registro.</param>
        /// <exception cref="ArgumentException">Si alg�n par�metro es inv�lido.</exception>
        public PropertyTraceAggregate(DateTime dateSale, string name, decimal value, decimal tax, int idProperty, string createdBy)
        {
            if (dateSale == default)
                throw new ArgumentException("La fecha de venta debe ser v�lida.", nameof(dateSale));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vac�o.", nameof(name));
            if (value <= 0)
                throw new ArgumentException("El valor debe ser mayor que cero.", nameof(value));
            if (tax < 0)
                throw new ArgumentException("El impuesto no puede ser negativo.", nameof(tax));
            if (idProperty <= 0)
                throw new ArgumentException("El identificador de la propiedad debe ser mayor que cero.", nameof(idProperty));
            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("El usuario creador no puede estar vac�o.", nameof(createdBy));

            DateSale = dateSale;
            Name = name;
            Value = value;
            Tax = tax;
            IdProperty = idProperty;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        /// <summary>
        /// Constructor protegido para compatibilidad con EF Core.
        /// </summary>
        protected PropertyTraceAggregate() { }
    }
}