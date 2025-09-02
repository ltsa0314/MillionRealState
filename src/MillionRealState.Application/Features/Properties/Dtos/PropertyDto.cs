namespace MillionRealState.Application.Features.Properties.Dtos
{
    public sealed record PropertyDto(
        int Id,
        string Name,
        AddressDto Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int OwnerId);

    public sealed record AddressDto(string Country, string City, string Neighborhood, string Street, string Number);

    public sealed record CreatePropertyDto(
    string Name,
    AddressDto Address,
    decimal Price,
    string CodeInternal,
    int Year,
    int OwnerId);

    public sealed record UpdatePropertyDto(
        string Name,
        AddressDto Address,
        string CodeInternal,
        int Year,
        int OwnerId);
}
