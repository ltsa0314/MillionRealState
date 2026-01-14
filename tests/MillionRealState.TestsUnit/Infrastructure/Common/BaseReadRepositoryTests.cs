using Xunit;
using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;

public class BaseReadRepositoryTests
{
    private TestMillionRealStateDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MillionRealStateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TestMillionRealStateDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsEntity()
    {
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        context.TestEntities.Add(entity);
        await context.SaveChangesAsync();

        var result = await repo.GetByIdAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        var context = CreateContext();
        var repo = new TestRepository(context);
        context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Test1" });
        context.TestEntities.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Test2" });
        await context.SaveChangesAsync();

        var all = await repo.GetAllAsync();

        Assert.Equal(2, all.Count());
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrueIfExists()
    {
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        context.TestEntities.Add(entity);
        await context.SaveChangesAsync();

        var exists = await repo.ExistsAsync(entity.Id);

        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalseIfNotExists()
    {
        var context = CreateContext();
        var repo = new TestRepository(context);

        var exists = await repo.ExistsAsync(Guid.NewGuid());

        Assert.False(exists);
    }
}