namespace MillionRealState.Application.Features.Property.Dtos
{
    /// <summary>
    /// Data Transfer Object for adding an image to a property.
    /// </summary>
    public class AddPropertyImageDto
    {
        /// <summary>
        /// Base64-encoded image file to be added to the property.
        /// </summary>
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the image is enabled or visible for the property.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
