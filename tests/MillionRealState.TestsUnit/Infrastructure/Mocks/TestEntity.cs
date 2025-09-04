using MillionRealState.Domain.SeedWork.Models;

public class TestEntity : AggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}