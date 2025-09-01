using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.Aggregates.Property
{
    /// <summary>
    /// Agregado raíz que representa una propiedad inmobiliaria.
    /// Contiene la información principal de la propiedad y la colección de imágenes asociadas.
    /// </summary>
    internal class PropertyAggregate : AggregateRoot
    {
        /// <summary>
        /// Identificador único de la propiedad.
        /// </summary>
        public int IdProperty { get; private set; }

        /// <summary>
        /// Nombre de la propiedad.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Dirección de la propiedad, representada como un Value Object.
        /// </summary>
        public AddressValueObject Address { get; private set; } = default!;

        /// <summary>
        /// Precio actual de la propiedad.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Código interno de la propiedad.
        /// </summary>
        public string CodeInternal { get; private set; } = string.Empty;

        /// <summary>
        /// Año de construcción o registro de la propiedad.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Identificador del propietario de la propiedad.
        /// </summary>
        public int IdOwner { get; private set; }

        /// <summary>
        /// Colección de imágenes asociadas a la propiedad.
        /// </summary>
        public List<PropertyImage> Images { get; private set; } = new();

        /// <summary>
        /// Constructor para crear una nueva propiedad inmobiliaria.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="address">Dirección de la propiedad.</param>
        /// <param name="price">Precio de la propiedad.</param>
        /// <param name="codeInternal">Código interno de la propiedad.</param>
        /// <param name="year">Año de la propiedad.</param>
        /// <param name="idOwner">Identificador del propietario.</param>
        /// <exception cref="ArgumentException">Si algún parámetro es inválido.</exception>
        /// <exception cref="ArgumentNullException">Si la dirección es nula.</exception>
        public PropertyAggregate(string name, AddressValueObject address, decimal price, string codeInternal, int year, int idOwner)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre de la propiedad no puede estar vacío.", nameof(name));
            if (address is null)
                throw new ArgumentNullException(nameof(address), "La dirección no puede ser nula.");
            if (price <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.", nameof(price));
            if (string.IsNullOrWhiteSpace(codeInternal))
                throw new ArgumentException("El código interno no puede estar vacío.", nameof(codeInternal));
            if (year <= 0)
                throw new ArgumentException("El año debe ser mayor que cero.", nameof(year));
            if (idOwner <= 0)
                throw new ArgumentException("El identificador del propietario debe ser mayor que cero.", nameof(idOwner));

            Name = name;
            Address = address;
            Price = price;
            CodeInternal = codeInternal;
            Year = year;
            IdOwner = idOwner;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Agrega una nueva imagen a la propiedad.
        /// </summary>
        /// <param name="file">Ruta o contenido del archivo de imagen.</param>
        /// <param name="enabled">Indica si la imagen está habilitada.</param>
        public void AddImage(string file, bool enabled)
        {
            var image = new PropertyImage(IdProperty, file, enabled);
            Images.Add(image);
        }

        /// <summary>
        /// Cambia el precio de la propiedad.
        /// </summary>
        /// <param name="newPrice">Nuevo precio.</param>
        /// <exception cref="ArgumentException">Si el nuevo precio es inválido.</exception>
        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("El nuevo precio debe ser mayor que cero.", nameof(newPrice));
            Price = newPrice;
        }

        /// <summary>
        /// Actualiza los datos principales de la propiedad.
        /// </summary>
        /// <param name="name">Nuevo nombre.</param>
        /// <param name="address">Nueva dirección.</param>
        /// <param name="codeInternal">Nuevo código interno.</param>
        /// <param name="year">Nuevo año.</param>
        /// <param name="idOwner">Nuevo identificador de propietario.</param>
        /// <exception cref="ArgumentException">Si algún parámetro es inválido.</exception>
        /// <exception cref="ArgumentNullException">Si la dirección es nula.</exception>    
        public void Update(string name, AddressValueObject address, string codeInternal, int year, int idOwner)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre de la propiedad no puede estar vacío.", nameof(name));
            if (address is null)
                throw new ArgumentNullException(nameof(address), "La dirección no puede ser nula.");
            if (string.IsNullOrWhiteSpace(codeInternal))
                throw new ArgumentException("El código interno no puede estar vacío.", nameof(codeInternal));
            if (year <= 0)
                throw new ArgumentException("El año debe ser mayor que cero.", nameof(year));
            if (idOwner <= 0)
                throw new ArgumentException("El identificador del propietario debe ser mayor que cero.", nameof(idOwner));

            Name = name;
            Address = address;
            CodeInternal = codeInternal;
            Year = year;
            IdOwner = idOwner;
        }
    }
}