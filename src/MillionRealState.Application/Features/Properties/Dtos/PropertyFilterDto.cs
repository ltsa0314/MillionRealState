namespace MillionRealState.Application.Features.Properties.Dtos
{
    public sealed record PropertyFilterDto(
        Guid? IdOwner = null,
        string? City = null,
        decimal? PriceMin = null,
        decimal? PriceMax = null,
        int? YearFrom = null,
        int? YearTo = null,
        string? Text = null,
        string? SortBy = "Name",
        string? SortDir = "asc",
        int Page = 1,
        int PageSize = 20);
}
