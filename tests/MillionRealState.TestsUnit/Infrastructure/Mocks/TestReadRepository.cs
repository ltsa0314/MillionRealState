using MillionRealState.Infrastructure.Common;

public class TestReadRepository : BaseReadRepository<TestEntity, Guid>
{
    public TestReadRepository(TestMillionRealStateDbContext context) : base(context) { }
}