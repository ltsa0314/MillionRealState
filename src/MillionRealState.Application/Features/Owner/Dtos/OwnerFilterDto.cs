namespace MillionRealState.Application.Features.Owner.Dtos
{
    /// <summary>
    /// Data Transfer Object for filtering and paginating owner queries.
    /// </summary>
    public class OwnerFilterDto
    {
        /// <summary>
        /// Filter by owner's name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Filter by city.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Filter by minimum birthday date.
        /// </summary>
        public DateTime? BirthdayFrom { get; set; }

        /// <summary>
        /// Filter by maximum birthday date.
        /// </summary>
        public DateTime? BirthdayTo { get; set; }

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