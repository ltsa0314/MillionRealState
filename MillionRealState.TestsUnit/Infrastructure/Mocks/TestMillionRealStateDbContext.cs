using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;

public class TestMillionRealStateDbContext : MillionRealStateDbContext
{
    public DbSet<TestEntity> TestEntities { get; set; }
    public TestMillionRealStateDbContext(DbContextOptions<MillionRealStateDbContext> options)
        : base(options) { }
}