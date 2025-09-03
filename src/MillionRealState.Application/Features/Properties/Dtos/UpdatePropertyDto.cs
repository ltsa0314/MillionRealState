using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Properties.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating an existing property.
    /// </summary>
    public class UpdatePropertyDto
    {
        /// <summary>
        /// Updated property name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Updated address information for the property.
        /// </summary>
        public AddressDto Address { get; set; } = default!;

        /// <summary>
        /// Updated internal code for the property.
        /// </summary>
        public string CodeInternal { get; set; } = string.Empty;

        /// <summary>
        /// Updated year for the property.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Unique identifier of the owner.
        /// </summary>
        public Guid IdOwner { get; set; }
    }
}
