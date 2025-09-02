namespace MillionRealState.Application.Features.Properties.Dtos
{
    public sealed record PropertyImageDto(int IdPropertyImage, string File, bool Enabled);

    public sealed record PropertyDto(
        int IdProperty,
        string Name,
        AddressDto Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int IdOwner,
        List<PropertyImageDto> Images,
        int TracesCount);

}
