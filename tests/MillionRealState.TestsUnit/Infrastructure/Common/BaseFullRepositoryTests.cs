using Xunit;
using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;

public class BaseFullRepositoryTests
{
    private TestMillionRealStateDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MillionRealStateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TestMillionRealStateDbContext(options);
    }

    [Fact]
    public async Task AddAndGetByIdAsync_WorksCorrectly()
    {
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

        await repo.AddAsync(entity);
        var result = await repo.GetByIdAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesEntity()
    {
        var context = CreateContext();
        var repo = new TestFullRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        await repo.AddAsync(entity);

        entity.Name = "Updated";
        await repo.UpdateAsync(entity);
        var result = await repo.GetByIdAsync(entity.Id);

        Assert.Equal("Updated", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesEntity()
    {
        var context = CreateContext();
        var repo = new TestFullRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        await repo.AddAsync(entity);

        await repo.DeleteAsync(entity.Id);
        var result = await repo.GetByIdAsync(entity.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        var context = CreateContext();
        var repo = new TestFullRepository(context);
        await repo.AddAsync(new TestEntity { Id = Guid.NewGuid(), Name = "Test1" });
        await repo.AddAsync(new TestEntity { Id = Guid.NewGuid(), Name = "Test2" });

        var all = await repo.GetAllAsync();

        Assert.Equal(2, all.Count());
    }
}