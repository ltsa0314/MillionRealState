namespace MillionRealState.Domain.Aggregates.Property
{
    /// <summary>
    /// Entidad que representa una imagen asociada a una propiedad inmobiliaria.
    /// </summary>
    public class PropertyImage
    {
        public int IdPropertyImage { get; private set; }
        public int IdProperty { get; private set; }
        public string File { get; private set; } = string.Empty;
        public bool Enabled { get; private set; }

        /// <summary>
        /// Propiedad de navegación a la propiedad (lazy loading).
        /// </summary>
        public virtual PropertyAggregate Property { get; protected set; } = default!;

        /// <summary>
        /// Constructor principal para crear una imagen de propiedad.
        /// </summary>
        public PropertyImage(int idProperty, string file, bool enabled)
        {
            if (idProperty <= 0)
                throw new ArgumentException("El identificador de la propiedad debe ser mayor que cero.", nameof(idProperty));
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentException("El archivo de imagen no puede estar vacío.", nameof(file));

            IdProperty = idProperty;
            File = file;
            Enabled = enabled;
        }

        /// <summary>
        /// Constructor protegido para compatibilidad con EF Core.
        /// </summary>
        protected PropertyImage() { }

        /// <summary>
        /// Permite habilitar o deshabilitar la imagen.
        /// </summary>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }
    }
}