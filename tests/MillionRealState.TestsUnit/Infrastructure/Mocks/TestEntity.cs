using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.TestsUnit.Infrastructure.Mocks
{
    public class TestEntity : AggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}