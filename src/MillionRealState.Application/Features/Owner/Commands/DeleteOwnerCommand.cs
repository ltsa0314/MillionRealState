using MediatR;

namespace MillionRealState.Application.Features.Owner.Commands
{
    public record DeleteOwnerCommand(Guid IdOwner) : IRequest;
}