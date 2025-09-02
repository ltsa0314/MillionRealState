using AutoMapper;
using FluentValidation;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Results;
using MillionRealState.Application.Features.Properties.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;

namespace MillionRealState.Application.Features.Properties.Services
{
    public sealed class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;
        private readonly IValidator<CreatePropertyDto> _createVal;
        private readonly IValidator<UpdatePropertyDto> _updateVal;
        private readonly IValidator<AddPropertyImageDto> _imgVal;
        private readonly IMapper _mapper;

        public PropertyService(
            IPropertyRepository repo,
            IValidator<CreatePropertyDto> createVal,
            IValidator<UpdatePropertyDto> updateVal,
            IValidator<AddPropertyImageDto> imgVal,
            IMapper mapper)
            => (_repo, _createVal, _updateVal, _imgVal, _mapper) = (repo, createVal, updateVal, imgVal, mapper);

        // Create Property Building
        public async Task<int> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default)
        {
            await _createVal.ValidateAndThrowAsync(dto, ct);

            var entity = _mapper.Map<PropertyAggregate>(dto);

            await _repo.AddAsync(entity);
            return entity.IdProperty;
        }

        // Add Image from property
        public async Task AddImageAsync(Guid idProperty, AddPropertyImageDto dto, CancellationToken ct = default)
        {
            await _imgVal.ValidateAndThrowAsync(dto, ct);

            var prop = await _repo.GetByIdAsync(idProperty)
                      ?? throw new KeyNotFoundException($"Property {idProperty} no existe.");

            prop.AddImage(dto.File, dto.Enabled);
            await _repo.UpdateAsync(prop);
        }

        // Change Price
        public async Task ChangePriceAsync(Guid idProperty, decimal newPrice, CancellationToken ct = default)
        {
            var prop = await _repo.GetByIdAsync(idProperty)
                      ?? throw new KeyNotFoundException($"Property {idProperty} no existe.");

            prop.ChangePrice(newPrice);
            await _repo.UpdateAsync(prop);
        }

        // Update property (usando mapper para Address VO)
        public async Task UpdateAsync(Guid idProperty, UpdatePropertyDto dto, CancellationToken ct = default)
        {
            await _updateVal.ValidateAndThrowAsync(dto, ct);

            var prop = await _repo.GetByIdAsync(idProperty)
                      ?? throw new KeyNotFoundException($"Property {idProperty} no existe.");

            var addressVO = _mapper.Map<AddressValueObject>(dto); // mapeo configurado arriba
            prop.Update(dto.Name, addressVO, dto.CodeInternal, dto.Year, dto.IdOwner);

            await _repo.UpdateAsync(prop);
        }

        public async Task<PropertyDto> GetByIdAsync(Guid idProperty, CancellationToken ct = default)
        {
            var prop = await _repo.GetByIdAsync(idProperty)
                      ?? throw new KeyNotFoundException($"Property {idProperty} no existe.");

            return _mapper.Map<PropertyDto>(prop);
        }

        public async Task<PagedResult<PropertyDto>> ListAsync(PropertyFilterDto f, CancellationToken ct = default)
        {
            var filter = _mapper.Map<PropertyFilter>(f);

            var (items, total) = await _repo.ListPagedAsync(filter, ct);

            var dtos = _mapper.Map<List<PropertyDto>>(items);

            return new PagedResult<PropertyDto>(dtos, total, filter.Page, filter.PageSize);
        }

    }
}