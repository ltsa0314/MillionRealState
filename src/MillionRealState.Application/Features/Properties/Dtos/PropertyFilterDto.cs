namespace MillionRealState.Application.Features.Properties.Dtos
{
    /// <summary>
    /// Data Transfer Object for filtering and paginating property queries.
    /// </summary>
    public class PropertyFilterDto
    {
        /// <summary>
        /// Filter by property name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Filter by city.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Minimum price filter.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Maximum price filter.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Filter by year.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Filter by owner identifier.
        /// </summary>
        public Guid? IdOwner { get; set; }

        /// <summary>
        /// Page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
