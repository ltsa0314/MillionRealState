using MediatR;
using MillionRealState.Application.Features.Owner.Dtos;

namespace MillionRealState.Application.Features.Owner.Queries
{
    public record GetOwnerByIdQuery(Guid IdOwner) : IRequest<OwnerDto>;
}