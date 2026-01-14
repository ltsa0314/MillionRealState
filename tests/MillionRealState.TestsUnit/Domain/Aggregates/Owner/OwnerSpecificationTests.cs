using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Owner.Specifications;

namespace MillionRealState.TestsUnit.Domain.Aggregates.Owner
{
    public class OwnerSpecificationTests
    {
        private List<OwnerAggregate> GetSampleOwners()
        {
            return
            [
                new OwnerAggregate(Guid.NewGuid(), "Juan Perez", new AddressValueObject("Calle 1", "Bogotá", "Cundinamarca", "1111", "Colombia"), "foto1.jpg", new DateTime(1980, 1, 1)),
            new OwnerAggregate(Guid.NewGuid(), "Ana Gómez", new AddressValueObject("Calle 2", "Medellín", "Antioquia", "2222", "Colombia"), "foto2.jpg", new DateTime(1990, 5, 10)),
            new OwnerAggregate(Guid.NewGuid(), "Carlos Ruiz", new AddressValueObject("Vereda", "Cali", "Valle", "3333", "Colombia"), "foto3.jpg", new DateTime(1975, 12, 25))
            ];
        }

        [Fact]
        public void OwnerByNameSpecification_FiltersByName()
        {
            var spec = new OwnerByNameSpecification("Juan");
            var owners = GetSampleOwners().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(owners);
            Assert.Contains("Juan", owners[0].Name);
        }

        [Fact]
        public void OwnerByCitySpecification_FiltersByCity()
        {
            var spec = new OwnerByCitySpecification("Medellín");
            var owners = GetSampleOwners().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(owners);
            Assert.Equal("Medellín", owners[0].Address.City);
        }

        [Fact]
        public void OwnerByBirthdayRangeSpecification_FiltersByBirthdayRange()
        {
            var range = new BirthdayRange { From = new DateTime(1985, 1, 1), To = new DateTime(1995, 12, 31) };
            var spec = new OwnerByBirthdayRangeSpecification(range);
            var owners = GetSampleOwners().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(owners);
            Assert.Equal(new DateTime(1990, 5, 10), owners[0].Birthday);
        }
    }
}