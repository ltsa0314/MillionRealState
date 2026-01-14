using MediatR;

namespace MillionRealState.Application.Features.Property.Commands
{
    public record DeletePropertyCommand(Guid IdProperty) : IRequest;
}