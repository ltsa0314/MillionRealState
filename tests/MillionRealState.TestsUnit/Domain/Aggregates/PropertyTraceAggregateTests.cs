using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.PropertyTrace;
using System;
using Xunit;

public class PropertyTraceAggregateTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesTrace()
    {
        // Arrange
        var dateSale = new DateTime(2024, 1, 1);
        var name = "Compra";
        var value = 500000m;
        var tax = 5000m;
        var idProperty = Guid.NewGuid();
        var createdBy = "admin";

        // Act
        var trace = new PropertyTraceAggregate(dateSale, name, value, tax, idProperty, createdBy);

        // Assert
        Assert.Equal(dateSale, trace.DateSale);
        Assert.Equal(name, trace.Name);
        Assert.Equal(value, trace.Value);
        Assert.Equal(tax, trace.Tax);
        Assert.Equal(idProperty, trace.IdProperty);
        Assert.Equal(createdBy, trace.CreatedBy);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ThrowsException(string invalidName)
    {
        // Arrange
        var dateSale = DateTime.Now;
        var value = 100000m;
        var tax = 1000m;
        var idProperty = Guid.NewGuid();
        var createdBy = "admin";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyTraceAggregate(dateSale, invalidName, value, tax, idProperty, createdBy));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Constructor_InvalidValue_ThrowsException(decimal invalidValue)
    {
        // Arrange
        var dateSale = DateTime.Now;
        var name = "Venta";
        var tax = 1000m;
        var idProperty = Guid.NewGuid();
        var createdBy = "admin";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyTraceAggregate(dateSale, name, invalidValue, tax, idProperty, createdBy));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Constructor_InvalidTax_ThrowsException(decimal invalidTax)
    {
        // Arrange
        var dateSale = DateTime.Now;
        var name = "Venta";
        var value = 100000m;
        var idProperty = Guid.NewGuid();
        var createdBy = "admin";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyTraceAggregate(dateSale, name, value, invalidTax, idProperty, createdBy));
    }

    [Fact]
    public void Constructor_EmptyIdProperty_ThrowsException()
    {
        // Arrange
        var dateSale = DateTime.Now;
        var name = "Venta";
        var value = 100000m;
        var tax = 1000m;
        var createdBy = "admin";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyTraceAggregate(dateSale, name, value, tax, Guid.Empty, createdBy));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidCreatedBy_ThrowsException(string invalidCreatedBy)
    {
        // Arrange
        var dateSale = DateTime.Now;
        var name = "Venta";
        var value = 100000m;
        var tax = 1000m;
        var idProperty = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new PropertyTraceAggregate(dateSale, name, value, tax, idProperty, invalidCreatedBy));
    }
}