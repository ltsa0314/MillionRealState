using MillionRealState.Infrastructure.Common;

namespace MillionRealState.TestsUnit.Infrastructure.Mocks
{
    public class TestReadRepository : BaseReadRepository<TestEntity, Guid>
    {
        public TestReadRepository(TestMillionRealStateDbContext context) : base(context) { }
    }
}