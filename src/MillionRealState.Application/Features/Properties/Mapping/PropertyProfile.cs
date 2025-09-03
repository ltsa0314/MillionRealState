using AutoMapper;
using MillionRealState.Application.Common.Dtos;
using MillionRealState.Application.Features.Properties.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Application.Features.Properties.Mapping
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {

            // PropertyFilterDto   -> PropertyFilter
            CreateMap<PropertyFilterDto, PropertyFilter>();

            // CreatePropertyDto -> PropertyAggregate
            CreateMap<CreatePropertyDto, PropertyAggregate>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // UpdatePropertyDto -> AddressValueObject
            CreateMap<UpdatePropertyDto, AddressValueObject>()
                .ConstructUsing(src => new AddressValueObject(
                    src.Address.Street,
                    src.Address.City,
                    src.Address.State,
                    src.Address.ZipCode,
                    src.Address.Country));

            // AddressDto <-> AddressValueObject
            CreateMap<AddressDto, AddressValueObject>()
                .ConstructUsing(src => new AddressValueObject(
                    src.Street,
                    src.City,
                    src.State,
                    src.ZipCode,
                    src.Country));
            CreateMap<AddressValueObject, AddressDto>();

            // PropertyAggregate -> PropertyDto
            CreateMap<PropertyAggregate, PropertyDto>();

            // PropertyImage <-> PropertyImageDto
            CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
        }
    }
}   