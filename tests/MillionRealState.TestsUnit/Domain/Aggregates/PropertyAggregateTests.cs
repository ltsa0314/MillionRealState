using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using System;
using Xunit;

public class PropertyAggregateTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesProperty()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");
        var name = "Casa";
        var price = 100000m;
        var codeInternal = "INT-001";
        var year = 2020;
        var ownerId = Guid.NewGuid();

        // Act
        var property = new PropertyAggregate(name, address, price, codeInternal, year, ownerId);
        
    
        // Assert
        Assert.Equal(name, property.Name);
        Assert.Equal(price, property.Price);
        Assert.Equal(address, property.Address);
        Assert.Equal(codeInternal, property.CodeInternal);
        Assert.Equal(year, property.Year);
        Assert.Equal(ownerId, property.IdOwner);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsException(string invalidName)
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyAggregate(invalidName, address, 100000, "INT-001", 2020, Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_NullAddress_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new PropertyAggregate("Casa", null, 100000, "INT-001", 2020, Guid.NewGuid()));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_InvalidPrice_ThrowsException(decimal invalidPrice)
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyAggregate("Casa", address, invalidPrice, "INT-001", 2020, Guid.NewGuid()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidCodeInternal_ThrowsException(string invalidCode)
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyAggregate("Casa", address, 100000, invalidCode, 2020, Guid.NewGuid()));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2020)]
    public void Constructor_InvalidYear_ThrowsException(int invalidYear)
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyAggregate("Casa", address, 100000, "INT-001", invalidYear, Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_EmptyOwnerId_ThrowsException()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyAggregate("Casa", address, 100000, "INT-001", 2020, Guid.Empty));
    }

    [Fact]
    public void ChangePrice_ValidPrice_UpdatesPrice()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");
        var property = new PropertyAggregate("Casa", address, 100000, "INT-001", 2020, Guid.NewGuid());
        var newPrice = 150000m;

        // Act
        property.ChangePrice(newPrice);

        // Assert
        Assert.Equal(newPrice, property.Price);
    }

    [Fact]
    public void ChangePrice_InvalidPrice_ThrowsException()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");
        var property = new PropertyAggregate("Casa", address, 100000, "INT-001", 2020, Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentException>(() => property.ChangePrice(0));
        Assert.Throws<ArgumentException>(() => property.ChangePrice(-100));
    }

    [Fact]
    public void Update_ValidParameters_UpdatesFields()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");
        var property = new PropertyAggregate("Casa", address, 100000, "INT-001", 2020, Guid.NewGuid());
        var newAddress = new AddressValueObject("Calle 2", "Ciudad2", "state2", "54321", "País2");
        var newName = "Apartamento";
        var newCode = "INT-002";
        var newYear = 2022;
        var newOwnerId = Guid.NewGuid();

        // Act
        property.Update(newName, newAddress, newCode, newYear, newOwnerId);

        // Assert
        Assert.Equal(newName, property.Name);
        Assert.Equal(newAddress, property.Address);
        Assert.Equal(newCode, property.CodeInternal);
        Assert.Equal(newYear, property.Year);
        Assert.Equal(newOwnerId, property.IdOwner);
    }

    [Fact]
    public void Update_InvalidParameters_ThrowsException()
    {
        // Arrange
        var address = new AddressValueObject("Calle 1", "Ciudad", "state", "12345", "País");
        var property = new PropertyAggregate("Casa", address, 100000, "INT-001", 2020, Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentException>(() => property.Update("", address, "INT-001", 2020, Guid.NewGuid()));
        Assert.Throws<ArgumentNullException>(() => property.Update("Casa", null, "INT-001", 2020, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => property.Update("Casa", address, "", 2020, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => property.Update("Casa", address, "INT-001", 0, Guid.NewGuid()));
        Assert.Throws<ArgumentException>(() => property.Update("Casa", address, "INT-001", 2020, Guid.Empty));
    }
}