using AutoMapper;
using FluentValidation;
using MillionRealState.Application.Abstractions.Services;
using MillionRealState.Application.Common.Results;
using MillionRealState.Application.Features.Owner.Dtos;
using MillionRealState.Domain.Aggregates.Owner;

namespace MillionRealState.Application.Features.Owners.Services
{
    public sealed class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repo;
        private readonly IValidator<CreateOwnerDto> _createVal;
        private readonly IValidator<UpdateOwnerDto> _updateVal;
        private readonly IMapper _mapper;

        public OwnerService(
            IOwnerRepository repo,
            IValidator<CreateOwnerDto> createVal,
            IValidator<UpdateOwnerDto> updateVal,
            IMapper mapper)
            => (_repo, _createVal, _updateVal, _mapper) = (repo, createVal, updateVal, mapper);

        public async Task<Guid > CreateAsync(CreateOwnerDto dto, CancellationToken ct = default)
        {
            await _createVal.ValidateAndThrowAsync(dto, ct);

            var entity = _mapper.Map<OwnerAggregate>(dto);

            await _repo.AddAsync(entity);
            return entity.IdOwner;
        }

        public async Task UpdateAsync(Guid idOwner, UpdateOwnerDto dto, CancellationToken ct = default)
        {
            await _updateVal.ValidateAndThrowAsync(dto, ct);

            var owner = await _repo.GetByIdAsync(idOwner)
                        ?? throw new KeyNotFoundException($"Owner {idOwner} no existe.");

            var addressVO = _mapper.Map<AddressValueObject>(dto.Address);
            owner.Update(dto.Name, addressVO, dto.Photo, dto.Birthday);

            await _repo.UpdateAsync(owner);
        }

        public async Task<OwnerDto> GetByIdAsync(Guid idOwner, CancellationToken ct = default)
        {
            var owner = await _repo.GetByIdAsync(idOwner)
                        ?? throw new KeyNotFoundException($"Owner {idOwner} no existe.");

            return _mapper.Map<OwnerDto>(owner);
        }

        public async Task<PagedResult<OwnerDto>> ListAsync(OwnerFilterDto f, CancellationToken ct = default)
        {
            var filter = _mapper.Map<OwnerFilter>(f);

            var (items, total) = await _repo.ListPagedAsync(filter, ct);

            var dtos = _mapper.Map<List<OwnerDto>>(items);


            return new PagedResult<OwnerDto>(dtos, total, filter.Page, filter.PageSize);
        }

    }
}