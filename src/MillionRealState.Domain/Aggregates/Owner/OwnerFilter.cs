namespace MillionRealState.Domain.Aggregates.Owner
{
    public class OwnerFilter
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public BirthdayRange? Birthday { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class BirthdayRange
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}