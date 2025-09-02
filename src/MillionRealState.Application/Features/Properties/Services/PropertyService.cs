using AutoMapper;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Exceptions;
using MillionRealState.Application.Features.Properties.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.SeedWork.Contracts;

namespace MillionRealState.Application.Features.Properties.Services
{
    public sealed class PropertyService : IPropertyService
    {
        private readonly IRepository<PropertyAggregate, int> _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePropertyDto>? _createValidator;
        private readonly IValidator<UpdatePropertyDto>? _updateValidator;

        public PropertyService(
            IRepository<PropertyAggregate, int> repo,
            IMapper mapper,
            IValidator<CreatePropertyDto>? createValidator = null,
            IValidator<UpdatePropertyDto>? updateValidator = null)
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<int> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default)
        {
            if (_createValidator is not null) await _createValidator.ValidateAndThrowAsync(dto, ct);

            var address = new AddressValueObject(
                dto.Address.Country, dto.Address.City, dto.Address.Neighborhood, dto.Address.Street, dto.Address.Number);

            var entity = new PropertyAggregate(
                dto.Name, address, dto.Price, dto.CodeInternal, dto.Year, dto.OwnerId);

            await _repo.AddAsync(entity); // tu repo hace SaveChanges adentro
            return entity.IdProperty;
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto dto, CancellationToken ct = default)
        {
            if (_updateValidator is not null) await _updateValidator.ValidateAndThrowAsync(dto, ct);

            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Property", id);

            var address = new AddressValueObject(
                dto.Address.Country, dto.Address.City, dto.Address.Neighborhood, dto.Address.Street, dto.Address.Number);

            entity.Update(dto.Name, address, dto.CodeInternal, dto.Year, dto.OwnerId);
            await _repo.UpdateAsync(entity); // SaveChanges adentro
        }

        public async Task ChangePriceAsync(int id, decimal newPrice, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Property", id);
            entity.ChangePrice(newPrice);
            await _repo.UpdateAsync(entity);
        }

        public async Task<PropertyDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException("Property", id);
            return _mapper.Map<PropertyDto>(entity);
        }

        public async Task<List<PropertyDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<List<PropertyDto>>(list.ToList());
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists) throw new NotFoundException("Property", id);
            await _repo.DeleteAsync(id);
        }
    }
}
