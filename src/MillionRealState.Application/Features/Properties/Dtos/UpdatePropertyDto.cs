    namespace MillionRealState.Application.Features.Properties.Dtos
{
    public sealed record UpdatePropertyDto(string Name, AddressDto Address, string CodeInternal, int Year, int OwnerId);
}
