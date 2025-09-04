using MillionRealState.Infrastructure.Common;

public class TestRepository : BaseRepository<TestEntity, Guid>
{
    public TestRepository(TestMillionRealStateDbContext context) : base(context) { }
}