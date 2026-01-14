using MillionRealState.Infrastructure.Common;

namespace MillionRealState.TestsUnit.Infrastructure.Mocks
{
    public class TestWriteRepository : BaseWriteRepository<TestEntity, Guid>
    {
        public TestWriteRepository(TestMillionRealStateDbContext context) : base(context) { }
    }
}