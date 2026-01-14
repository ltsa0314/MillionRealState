using MillionRealState.Infrastructure.Common;

public class TestWriteRepository : BaseWriteRepository<TestEntity, Guid>
{
    public TestWriteRepository(TestMillionRealStateDbContext context) : base(context) { }
}