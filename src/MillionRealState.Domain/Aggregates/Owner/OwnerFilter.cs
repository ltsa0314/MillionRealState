namespace MillionRealState.Domain.Aggregates.Owner
{
    public class OwnerFilter
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public DateTime? BirthdayFrom { get; set; }
        public DateTime? BirthdayTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}