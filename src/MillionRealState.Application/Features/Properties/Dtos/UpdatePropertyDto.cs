using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Properties.Dtos
{
    public class UpdatePropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = default!;
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int IdOwner { get; set; } 
    }
}
