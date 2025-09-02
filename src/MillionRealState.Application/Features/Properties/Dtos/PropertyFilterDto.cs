namespace MillionRealState.Application.Features.Properties.Dtos
{
    public class PropertyFilterDto
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Year { get; set; }
        public int? IdOwner { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
