namespace MillionRealState.Application.Features.Owner.Dtos
{
    public class OwnerFilterDto
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public DateTime? BirthdayFrom { get; set; }
        public DateTime? BirthdayTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}