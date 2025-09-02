using AutoMapper;
using MillionRealState.Application.Features.Properties.Dtos;
using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Property;

namespace MillionRealState.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PropertyAggregate, PropertyDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdProperty))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.IdOwner));
            CreateMap<AddressValueObject, AddressDto>();
        }
    }
}
