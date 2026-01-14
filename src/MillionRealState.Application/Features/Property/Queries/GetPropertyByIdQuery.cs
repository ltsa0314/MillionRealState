using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Queries
{
    public record GetPropertyByIdQuery(Guid IdProperty) : IRequest<PropertyDto>;
}