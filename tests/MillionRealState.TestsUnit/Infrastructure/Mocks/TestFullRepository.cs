using MillionRealState.Infrastructure.Common;

public class TestFullRepository : BaseRepository<TestEntity, Guid>
{
    public TestFullRepository(TestMillionRealStateDbContext context) : base(context) { }
}