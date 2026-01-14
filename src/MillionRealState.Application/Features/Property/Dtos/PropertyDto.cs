using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Property.Dtos
{
    /// <summary>
    /// Data Transfer Object representing a property.
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// Unique identifier of the property.
        /// </summary>
        public Guid IdProperty { get; set; }

        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Address information for the property.
        /// </summary>
        public AddressDto Address { get; set; } = default!;

        /// <summary>
        /// Current price of the property.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Internal code for the property.
        /// </summary>
        public string CodeInternal { get; set; } = string.Empty;

        /// <summary>
        /// Year the property was built or registered.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Unique identifier of the owner.
        /// </summary>
        public Guid IdOwner { get; set; }

        /// <summary>
        /// List of images associated with the property.
        /// </summary>
        public List<PropertyImageDto> Images { get; set; } = new();
    }

    /// <summary>
    /// Data Transfer Object representing a property image.
    /// </summary>
    public class PropertyImageDto
    {
        /// <summary>
        /// Unique identifier of the property image.
        /// </summary>
        public Guid IdPropertyImage { get; set; }

        /// <summary>
        /// Base64-encoded image file.
        /// </summary>
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the image is enabled or visible for the property.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
