using Moq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MillionRealState.Application.Features.Properties.Services;
using MillionRealState.Application.Features.Properties.Dtos;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Application.Common.Dtos;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PropertyServiceTests
{
    private readonly Mock<IPropertyRepository> _repoMock = new();
    private readonly Mock<IValidator<CreatePropertyDto>> _createValMock = new();
    private readonly Mock<IValidator<UpdatePropertyDto>> _updateValMock = new();
    private readonly Mock<IValidator<AddPropertyImageDto>> _imgValMock = new();
    private readonly Mock<IValidator<ChangePriceDto>> _priceValMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private PropertyService CreateService() =>
        new PropertyService(_repoMock.Object, _createValMock.Object, _updateValMock.Object, _imgValMock.Object, _priceValMock.Object, _mapperMock.Object);

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsId()
    {
        // Arrange
        var dto = new CreatePropertyDto();
        var entity = new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid());
        _mapperMock.Setup(m => m.Map<PropertyAggregate>(dto)).Returns(entity);
        _createValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.Equal(entity.IdProperty, result);
    }

    [Fact]
    public async Task AddImageAsync_ValidDto_AddsImage()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var dto = new AddPropertyImageDto { File = "img.jpg", Enabled = true };
        var property = new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid());

        // Asegura que IdProperty no sea Guid.Empty
        property.GetType().GetProperty("IdProperty")!.SetValue(property, idProperty);

        _imgValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.GetByIdAsync(idProperty)).ReturnsAsync(property);
        _repoMock.Setup(r => r.UpdateAsync(property)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        await service.AddImageAsync(idProperty, dto);

        // Assert
        _repoMock.Verify(r => r.UpdateAsync(property), Times.Once);
        Assert.Contains(property.Images, img => img.File == dto.File && img.Enabled == dto.Enabled);
    }

    [Fact]
    public async Task AddImageAsync_PropertyNotFound_ThrowsException()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var dto = new AddPropertyImageDto { File = "img.jpg", Enabled = true };
        _imgValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.GetByIdAsync(idProperty)).ReturnsAsync((PropertyAggregate)null);

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.AddImageAsync(idProperty, dto));
    }

    [Fact]
    public async Task ChangePriceAsync_ValidDto_ChangesPrice()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var dto = new ChangePriceDto { NewPrice = 200000 };
        var property = new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid());

        _priceValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.GetByIdAsync(idProperty)).ReturnsAsync(property);
        _repoMock.Setup(r => r.UpdateAsync(property)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        await service.ChangePriceAsync(idProperty, dto);

        // Assert
        _repoMock.Verify(r => r.UpdateAsync(property), Times.Once);
        Assert.Equal(dto.NewPrice, property.Price);
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_UpdatesProperty()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var dto = new UpdatePropertyDto
        {
            Name = "Nueva Casa",
            Address = new AddressDto { Street = "Nueva", City = "Ciudad", State = "State", ZipCode = "12345", Country = "País" },
            CodeInternal = "INT-002",
            Year = 2021,
            IdOwner = Guid.NewGuid()
        };
        var property = new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid());
        var addressVO = new AddressValueObject(dto.Address.Street, dto.Address.City, dto.Address.State, dto.Address.ZipCode, dto.Address.Country);

        _updateValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.GetByIdAsync(idProperty)).ReturnsAsync(property);
        _mapperMock.Setup(m => m.Map<AddressValueObject>(dto.Address)).Returns(addressVO);
        _repoMock.Setup(r => r.UpdateAsync(property)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        await service.UpdateAsync(idProperty, dto);

        // Assert
        _repoMock.Verify(r => r.UpdateAsync(property), Times.Once);
        Assert.Equal(dto.Name, property.Name);
        Assert.Equal(addressVO, property.Address);
        Assert.Equal(dto.CodeInternal, property.CodeInternal);
        Assert.Equal(dto.Year, property.Year);
        Assert.Equal(dto.IdOwner, property.IdOwner);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsPropertyDto()
    {
        // Arrange
        var idProperty = Guid.NewGuid();
        var property = new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid());
        var dto = new PropertyDto { IdProperty = idProperty, Name = "Casa" };

        _repoMock.Setup(r => r.GetByIdAsync(idProperty)).ReturnsAsync(property);
        _mapperMock.Setup(m => m.Map<PropertyDto>(property)).Returns(dto);

        var service = CreateService();

        // Act
        var result = await service.GetByIdAsync(idProperty);

        // Assert
        Assert.Equal(dto, result);
    }

    [Fact]
    public async Task ListAsync_ReturnsPagedResult()
    {
        // Arrange
        var filterDto = new PropertyFilterDto { Page = 1, PageSize = 10 };
        var filter = new PropertyFilter();
        var properties = new List<PropertyAggregate>
        {
            new PropertyAggregate("Casa", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), 100000, "INT-001", 2020, Guid.NewGuid())
        };
        var propertyDtos = new List<PropertyDto>
        {
            new PropertyDto { IdProperty = Guid.NewGuid(), Name = "Casa" }
        };
        var total = 1;

        _mapperMock.Setup(m => m.Map<PropertyFilter>(filterDto)).Returns(filter);
        _repoMock.Setup(r => r.ListPagedAsync(filter, default)).ReturnsAsync((properties, total));
        _mapperMock.Setup(m => m.Map<List<PropertyDto>>(properties)).Returns(propertyDtos);

        var service = CreateService();

        // Act
        var result = await service.ListAsync(filterDto);

        // Assert
        Assert.Equal(propertyDtos, result.Items);
        Assert.Equal(total, result.TotalCount);
        Assert.Equal(filter.Page, result.Page);
        Assert.Equal(filter.PageSize, result.PageSize);
    }
}