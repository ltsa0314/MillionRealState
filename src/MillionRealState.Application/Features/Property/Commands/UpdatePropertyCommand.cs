using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Commands
{
    public record UpdatePropertyCommand(Guid IdProperty, UpdatePropertyDto Property) : IRequest;
}