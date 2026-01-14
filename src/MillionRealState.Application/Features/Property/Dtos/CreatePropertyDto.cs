using MillionRealState.Application.Common.Dtos;
using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Property.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new property.
    /// </summary>
    public class CreatePropertyDto
    {
        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Address information for the property.
        /// </summary>
        public AddressDto Address { get; set; } = default!;

        /// <summary>
        /// Initial price of the property.
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
    }
}
