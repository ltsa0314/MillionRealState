using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Queries
{
    public record ListPropertiesQuery(PropertyFilterDto Filter) : IRequest<List<PropertyDto>>;
}