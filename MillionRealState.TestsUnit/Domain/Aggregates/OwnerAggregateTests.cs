using MillionRealState.Domain.Aggregates.Owner;

public class OwnerAggregateTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesOwner()
    {
        // Arrange
        var idOwner = Guid.NewGuid();
        var name = "Juan Pérez";
        var address = new AddressValueObject("Calle 1", "Ciudad", "State", "12345", "País");
        var photo = "http://photo.com/img.jpg";
        var birthday = new DateTime(1990, 5, 20);

        // Act
        var owner = new OwnerAggregate(idOwner, name, address, photo, birthday);

        // Assert
        Assert.Equal(idOwner, owner.IdOwner);
        Assert.Equal(name, owner.Name);
        Assert.Equal(address, owner.Address);
        Assert.Equal(photo, owner.Photo);
        Assert.Equal(birthday, owner.Birthday);
    }

    [Fact]
    public void Update_ValidParameters_UpdatesFields()
    {
        // Arrange
        var owner = new OwnerAggregate(Guid.NewGuid(), "Juan Pérez", new AddressValueObject("Calle 1", "Ciudad", "State", "12345", "País"), null, new DateTime(1990, 5, 20));
        var newName = "Ana Gómez";
        var newAddress = new AddressValueObject("Calle 2", "Ciudad2", "State2", "54321", "País2");
        var newPhoto = "http://photo.com/newimg.jpg";
        var newBirthday = new DateTime(1985, 10, 10);

        // Act
        owner.Update(newName, newAddress, newPhoto, newBirthday);

        // Assert
        Assert.Equal(newName, owner.Name);
        Assert.Equal(newAddress, owner.Address);
        Assert.Equal(newPhoto, owner.Photo);
        Assert.Equal(newBirthday, owner.Birthday);
    }

    [Fact]
    public void UpdateAddress_ValidAddress_UpdatesAddress()
    {
        // Arrange
        var owner = new OwnerAggregate(Guid.NewGuid(), "Juan Pérez", new AddressValueObject("Calle 1", "Ciudad", "State", "12345", "País"), null, new DateTime(1990, 5, 20));
        var newAddress = new AddressValueObject("Calle 3", "Ciudad3", "State3", "99999", "País3");

        // Act
        owner.UpdateAddress(newAddress);

        // Assert
        Assert.Equal(newAddress, owner.Address);
    }

    [Fact]
    public void UpdatePhoto_ValidPhoto_UpdatesPhoto()
    {
        // Arrange
        var owner = new OwnerAggregate(Guid.NewGuid(), "Juan Pérez", new AddressValueObject("Calle 1", "Ciudad", "State", "12345", "País"), null, new DateTime(1990, 5, 20));
        var newPhoto = "http://photo.com/updated.jpg";

        // Act
        owner.UpdatePhoto(newPhoto);

        // Assert
        Assert.Equal(newPhoto, owner.Photo);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_AllowsEmptyOrNull(string invalidName)
    {
        // Arrange
        var idOwner = Guid.NewGuid();
        var address = new AddressValueObject("Calle 1", "Ciudad", "State", "12345", "País");
        var birthday = new DateTime(1990, 5, 20);

        // Act
        var owner = new OwnerAggregate(idOwner, invalidName, address, null, birthday);

        // Assert
        Assert.Equal(invalidName, owner.Name);
    }
}