using Xunit;
using Microsoft.EntityFrameworkCore;
using MillionRealState.Infrastructure.Data.Context;
using MillionRealState.TestsUnit.Infrastructure.Mocks;

namespace MillionRealState.TestsUnit.Infrastructure.Common
{
    public class BaseWriteRepositoryTests
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
            var context = CreateContext();
            var repo = new TestWriteRepository(context);
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            await repo.AddAsync(entity);
            var result = await context.TestEntities.FindAsync(entity.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            var context = CreateContext();
            var repo = new TestWriteRepository(context);
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
            context.TestEntities.Add(entity);
            await context.SaveChangesAsync();

            await repo.DeleteAsync(entity.Id);
            var result = await context.TestEntities.FindAsync(entity.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            var context = CreateContext();
            var repo = new TestWriteRepository(context);
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };
            context.TestEntities.Add(entity);
            await context.SaveChangesAsync();

            entity.Name = "Updated";
            await repo.UpdateAsync(entity);
            var result = await context.TestEntities.FindAsync(entity.Id);

            Assert.Equal("Updated", result.Name);
        }
    }
}