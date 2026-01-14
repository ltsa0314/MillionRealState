using MillionRealState.Domain.Aggregates.Property;

namespace MillionRealState.TestsUnit.Domain.Aggregates
{
    public class PropertyImageTests
    {
        [Fact]
        public void Constructor_ValidParameters_CreatesImage()
        {
            // Arrange
            var idProperty = Guid.NewGuid();
            var file = "imagen.jpg";
            var enabled = true;

            // Act
            var image = new PropertyImage(idProperty, file, enabled);

            // Assert
            Assert.Equal(idProperty, image.IdProperty);
            Assert.Equal(file, image.File);
            Assert.Equal(enabled, image.Enabled);
        }

        [Fact]
        public void Constructor_EmptyIdProperty_ThrowsException()
        {
            // Arrange
            var file = "imagen.jpg";
            var enabled = true;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PropertyImage(Guid.Empty, file, enabled));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidFile_ThrowsException(string invalidFile)
        {
            // Arrange
            var idProperty = Guid.NewGuid();
            var enabled = true;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PropertyImage(idProperty, invalidFile, enabled));
        }

        [Fact]
        public void SetEnabled_ChangesEnabledState()
        {
            // Arrange
            var idProperty = Guid.NewGuid();
            var image = new PropertyImage(idProperty, "imagen.jpg", false);

            // Act
            image.SetEnabled(true);

            // Assert
            Assert.True(image.Enabled);

            // Act
            image.SetEnabled(false);

            // Assert
            Assert.False(image.Enabled);
        }
    }
}