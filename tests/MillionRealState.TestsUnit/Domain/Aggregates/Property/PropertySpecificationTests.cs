
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.Property.Specifications;

namespace MillionRealState.TestsUnit.Domain.Aggregates.Property
{
    public class PropertySpecificationTests
    {
        private List<PropertyAggregate> GetSampleProperties()
        {
            return new List<PropertyAggregate>
        {
            new PropertyAggregate("Casa Bonita", new AddressValueObject("Calle 1", "Bogotá", "Cundinamarca", "1111", "Colombia"), 100000, "A001", 2020, Guid.NewGuid()),
            new PropertyAggregate("Apartamento Central", new AddressValueObject("Calle 2", "Medellín", "Antioquia", "2222", "Colombia"), 200000, "A002", 2018, Guid.NewGuid()),
            new PropertyAggregate("Finca Verde", new AddressValueObject("Vereda", "Cali", "Valle", "3333", "Colombia"), 300000, "A003", 2015, Guid.NewGuid())
        };
        }

        [Fact]
        public void PropertyByNameSpecification_FiltersByName()
        {
            var spec = new PropertyByNameSpecification("Casa");
            var properties = GetSampleProperties().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(properties);
            Assert.Contains("Casa", properties[0].Name);
        }

        [Fact]
        public void PropertyByCitySpecification_FiltersByCity()
        {
            var spec = new PropertyByCitySpecification("Medellín");
            var properties = GetSampleProperties().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(properties);
            Assert.Equal("Medellín", properties[0].Address.City);
        }

        [Fact]
        public void PropertyByPriceRangeSpecification_FiltersByPriceRange()
        {
            var spec = new PropertyByPriceRangeSpecification(150000, 250000);
            var properties = GetSampleProperties().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(properties);
            Assert.Equal(200000, properties[0].Price);
        }

        [Fact]
        public void PropertyByYearRangeSpecification_FiltersByYearRange()
        {
            var spec = new PropertyByYearRangeSpecification(2016, 2021);
            var properties = GetSampleProperties().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Equal(2, properties.Count);
            Assert.All(properties, p => Assert.InRange(p.Year, 2016, 2021));
        }

        [Fact]
        public void PropertyByOwnerSpecification_FiltersByOwner()
        {
            var sample = GetSampleProperties();
            var ownerId = sample[1].IdOwner;
            var spec = new PropertyByOwnerSpecification(ownerId);
            var properties = sample.AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(properties);
            Assert.Equal(ownerId, properties[0].IdOwner);
        }

        [Fact]
        public void PropertyByTextSpecification_FiltersByText()
        {
            var spec = new PropertyByTextSpecification("Verde");
            var properties = GetSampleProperties().AsQueryable().Where(spec.Criteria).ToList();

            Assert.Single(properties);
            Assert.Contains("Verde", properties[0].Name);
        }
    }
}