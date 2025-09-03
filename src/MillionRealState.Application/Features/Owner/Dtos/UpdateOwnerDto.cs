using MillionRealState.Application.Common.Dtos;

namespace MillionRealState.Application.Features.Owner.Dtos
{
    public class UpdateOwnerDto
    {
        public string Name { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = default!;
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}