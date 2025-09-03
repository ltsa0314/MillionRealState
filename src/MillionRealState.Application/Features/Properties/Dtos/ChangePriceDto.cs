namespace MillionRealState.Application.Features.Properties.Dtos
{
    /// <summary>
    /// Data Transfer Object for changing the price of a property.
    /// </summary>
    public class ChangePriceDto
    {
        /// <summary>
        /// New price to set for the property.
        /// </summary>
        public decimal NewPrice { get; set; }
    }
}