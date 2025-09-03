using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Properties.Dtos
{
    public class CreatePropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
