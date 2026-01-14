using MillionRealState.Domain.Aggregates.Owner;
using System;
using Xunit;
namespace MillionRealState.TestsUnit.Domain.SeedWork.Models
{

    public class AddressValueObjectTests
    {
        [Fact]
        public void Constructor_ValidParameters_CreatesAddress()
        {
            // Arrange
            var street = "Calle 1";
            var city = "Ciudad";
            var state = "Estado";
            var zipCode = "12345";
            var country = "País";

            // Act
            var address = new AddressValueObject(street, city, state, zipCode, country);

            // Assert
            Assert.Equal(street, address.Street);
            Assert.Equal(city, address.City);
            Assert.Equal(state, address.State);
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(country, address.Country);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidStreet_ThrowsException(string invalidStreet)
        {
            // Arrange
            var city = "Ciudad";
            var state = "Estado";
            var zipCode = "12345";
            var country = "País";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new AddressValueObject(invalidStreet, city, state, zipCode, country));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidCity_ThrowsException(string invalidCity)
        {
            // Arrange
            var street = "Calle 1";
            var state = "Estado";
            var zipCode = "12345";
            var country = "País";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new AddressValueObject(street, invalidCity, state, zipCode, country));
        }

        [Fact]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Arrange
            var address1 = new AddressValueObject("Calle 1", "Ciudad", "Estado", "12345", "País");
            var address2 = new AddressValueObject("Calle 1", "Ciudad", "Estado", "12345", "País");

            // Act
            var result = address1.Equals(address2);

            // Assert
            Assert.True(result);
            Assert.Equal(address1, address2);
        }

        [Fact]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var address1 = new AddressValueObject("Calle 1", "Ciudad", "Estado", "12345", "País");
            var address2 = new AddressValueObject("Calle 2", "Ciudad", "Estado", "12345", "País");

            // Act
            var result = address1.Equals(address2);

            // Assert
            Assert.False(result);
            Assert.NotEqual(address1, address2);
        }

        [Fact]
        public void GetHashCode_SameValues_ReturnsSameHash()
        {
            // Arrange
            var address1 = new AddressValueObject("Calle 1", "Ciudad", "Estado", "12345", "País");
            var address2 = new AddressValueObject("Calle 1", "Ciudad", "Estado", "12345", "País");

            // Act
            var hash1 = address1.GetHashCode();
            var hash2 = address2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }
    }
}