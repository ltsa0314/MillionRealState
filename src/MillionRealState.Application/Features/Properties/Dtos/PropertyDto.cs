    namespace MillionRealState.Application.Features.Properties.Dtos
{
    public class PropertyDto
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public List<PropertyImageDto> Images { get; set; } = new();
    }

    public class PropertyImageDto
    {
        public int IdPropertyImage { get; set; }
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
