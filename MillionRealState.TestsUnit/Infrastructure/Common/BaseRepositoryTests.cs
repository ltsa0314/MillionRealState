using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;

public class BaseRepositoryTests
{
    private TestMillionRealStateDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MillionRealStateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TestMillionRealStateDbContext(options);
    }

    [Fact]
    public async Task AddAsync_AddsEntity()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

        // Act
        await repo.AddAsync(entity);
        var result = await repo.GetByIdAsync(entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesEntity()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        await repo.AddAsync(entity);

        // Act
        await repo.DeleteAsync(entity.Id);
        var result = await repo.GetByIdAsync(entity.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrueIfExists()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        await repo.AddAsync(entity);

        // Act
        var exists = await repo.ExistsAsync(entity.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalseIfNotExists()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);

        // Act
        var exists = await repo.ExistsAsync(Guid.NewGuid());

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesEntity()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
        await repo.AddAsync(entity);

        // Act
        entity.Name = "Updated";
        await repo.UpdateAsync(entity);
        var result = await repo.GetByIdAsync(entity.Id);

        // Assert
        Assert.Equal("Updated", result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        // Arrange
        var context = CreateContext();
        var repo = new TestRepository(context);
        await repo.AddAsync(new TestEntity { Id = Guid.NewGuid(), Name = "Test1" });
        await repo.AddAsync(new TestEntity { Id = Guid.NewGuid(), Name = "Test2" });

        // Act
        var all = await repo.GetAllAsync();

        // Assert
        Assert.Equal(2, all.Count());
    }
}