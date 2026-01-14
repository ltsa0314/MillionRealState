using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;

namespace MillionRealState.TestsUnit.Infrastructure.Mocks
{
    public class TestMillionRealStateDbContext : MillionRealStateDbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
        public TestMillionRealStateDbContext(DbContextOptions<MillionRealStateDbContext> options)
            : base(options) { }
    }
}