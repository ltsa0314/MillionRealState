namespace MillionRealState.Domain.Aggregates.Property
{
    public sealed record PropertyFilter(
        int? IdOwner = null,
        string? City = null,
        decimal? PriceMin = null,
        decimal? PriceMax = null,
        int? YearFrom = null,
        int? YearTo = null,
        string? Text = null,      
           int Page = 1,
        int PageSize = 20);
}
