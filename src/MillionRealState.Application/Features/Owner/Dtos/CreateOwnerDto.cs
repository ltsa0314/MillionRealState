using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Owner.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new owner.
    /// </summary>
    public class CreateOwnerDto
    {
        /// <summary>
        /// Owner's full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Address information for the owner.
        /// </summary>
        public AddressDto Address { get; set; } = default!;

        /// <summary>
        /// Optional photo URL or base64 string for the owner.
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Owner's date of birth.
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}