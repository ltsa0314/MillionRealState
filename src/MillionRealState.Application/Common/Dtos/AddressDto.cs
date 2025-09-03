namespace MillionRealState.Application.Common.Dtos
{
    /// <summary>
    /// Data Transfer Object representing an address.
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Street name and number.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// City name.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// State or province.
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Zip or postal code.
        /// </summary>
        public string ZipCode { get; set; } = string.Empty;

        /// <summary>
        /// Country name.
        /// </summary>
        public string Country { get; set; } = string.Empty;
    }
}
