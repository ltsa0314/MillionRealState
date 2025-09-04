using Moq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MillionRealState.Application.Features.Owners.Services;
using MillionRealState.Application.Features.Owner.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MillionRealState.Application.Common.Dtos;

public class OwnerServiceTests
{
    private readonly Mock<IOwnerRepository> _repoMock = new();
    private readonly Mock<IValidator<CreateOwnerDto>> _createValMock = new();
    private readonly Mock<IValidator<UpdateOwnerDto>> _updateValMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private OwnerService CreateService() =>
        new OwnerService(_repoMock.Object, _createValMock.Object, _updateValMock.Object, _mapperMock.Object);

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsId()
    {
        // Arrange
        var dto = new CreateOwnerDto();
        var entity = new OwnerAggregate(Guid.NewGuid(), "Juan", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), null, DateTime.Now);
        _mapperMock.Setup(m => m.Map<OwnerAggregate>(dto)).Returns(entity);
        _createValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.AddAsync(entity)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.Equal(entity.IdOwner, result);
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_UpdatesOwner()
    {
        // Arrange
        var idOwner = Guid.NewGuid();
        var dto = new UpdateOwnerDto
        {
            Name = "Ana",
            Address = new AddressDto { Street = "Nueva", City = "Ciudad", State = "State", ZipCode = "12345", Country = "País" },
            Photo = "photo.jpg",
            Birthday = new DateTime(1990, 1, 1)
        };
        var owner = new OwnerAggregate(idOwner, "Juan", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), null, DateTime.Now);
        var addressVO = new AddressValueObject(dto.Address.Street, dto.Address.City, dto.Address.State, dto.Address.ZipCode, dto.Address.Country);

        _updateValMock.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
        _repoMock.Setup(r => r.GetByIdAsync(idOwner)).ReturnsAsync(owner);
        _mapperMock.Setup(m => m.Map<AddressValueObject>(dto.Address)).Returns(addressVO);
        _repoMock.Setup(r => r.UpdateAsync(owner)).Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        await service.UpdateAsync(idOwner, dto);

        // Assert
        _repoMock.Verify(r => r.UpdateAsync(owner), Times.Once);
        Assert.Equal(dto.Name, owner.Name);
        Assert.Equal(addressVO, owner.Address);
        Assert.Equal(dto.Photo, owner.Photo);
        Assert.Equal(dto.Birthday, owner.Birthday);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsOwnerDto()
    {
        // Arrange
        var idOwner = Guid.NewGuid();
        var owner = new OwnerAggregate(idOwner, "Juan", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), null, DateTime.Now);
        var dto = new OwnerDto { IdOwner = idOwner, Name = "Juan" };

        _repoMock.Setup(r => r.GetByIdAsync(idOwner)).ReturnsAsync(owner);
        _mapperMock.Setup(m => m.Map<OwnerDto>(owner)).Returns(dto);

        var service = CreateService();

        // Act
        var result = await service.GetByIdAsync(idOwner);

        // Assert
        Assert.Equal(dto, result);
    }

    [Fact]
    public async Task ListAsync_ReturnsPagedResult()
    {
        // Arrange
        var filterDto = new OwnerFilterDto { Page = 1, PageSize = 10 };
        var filter = new OwnerFilter();
        var owners = new List<OwnerAggregate>
        {
            new OwnerAggregate(Guid.NewGuid(), "Juan", new AddressValueObject("Calle", "Ciudad", "State", "12345", "País"), null, DateTime.Now)
        };
        var ownerDtos = new List<OwnerDto>
        {
            new OwnerDto { IdOwner = Guid.NewGuid(), Name = "Juan" }
        };
        var total = 1;

        _mapperMock.Setup(m => m.Map<OwnerFilter>(filterDto)).Returns(filter);
        _repoMock.Setup(r => r.ListPagedAsync(filter, default)).ReturnsAsync((owners, total));
        _mapperMock.Setup(m => m.Map<List<OwnerDto>>(owners)).Returns(ownerDtos);

        var service = CreateService();

        // Act
        var result = await service.ListAsync(filterDto);

        // Assert
        Assert.Equal(ownerDtos, result.Items);
        Assert.Equal(total, result.TotalCount);
        Assert.Equal(filter.Page, result.Page);
        Assert.Equal(filter.PageSize, result.PageSize);
    }
}