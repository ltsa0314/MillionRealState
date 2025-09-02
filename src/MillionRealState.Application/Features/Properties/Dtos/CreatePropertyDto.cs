namespace MillionRealState.Application.Features.Properties.Dtos
{
    public sealed record CreatePropertyDto(string Name, AddressDto Address, decimal Price, string CodeInternal, int Year, int OwnerId);

}
