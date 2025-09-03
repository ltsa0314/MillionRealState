using AutoMapper;
using MillionRealState.Application.Common.Dtos;
using MillionRealState.Application.Features.Owner.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Application.Features.Owner.Mapping
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            //  OwnerFilterDto ->  OwnerFilterDto
            CreateMap<OwnerFilterDto, OwnerFilter>();

            // CreateOwnerDto -> OwnerAggregate
            CreateMap<CreateOwnerDto, OwnerAggregate>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // UpdateOwnerDto -> OwnerAggregate
            CreateMap<UpdateOwnerDto, OwnerAggregate>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // OwnerAggregate -> OwnerDto
            CreateMap<OwnerAggregate, OwnerDto>();

            // AddressDto <-> AddressValueObject
            CreateMap<AddressDto, AddressValueObject>()
                .ConstructUsing(src => new AddressValueObject(
                    src.Street,
                    src.City,
                    src.State,
                    src.ZipCode,
                    src.Country));
            CreateMap<AddressValueObject, AddressDto>();
        }
    }
}