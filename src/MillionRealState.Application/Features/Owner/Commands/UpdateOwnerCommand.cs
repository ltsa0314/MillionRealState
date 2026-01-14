using MediatR;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.Commands
{
    public record UpdateOwnerCommand(Guid IdOwner, UpdateOwnerDto Owner) : IRequest;
}