using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Owner.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating an existing owner.
    /// </summary>
    public class UpdateOwnerDto
    {
        /// <summary>
        /// Updated owner's full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Updated address information for the owner.
        /// </summary>
        public AddressDto Address { get; set; } = default!;

        /// <summary>
        /// Optional updated photo URL or base64 string for the owner.
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Updated owner's date of birth.
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}