using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Commands
{
    public record CreatePropertyCommand(CreatePropertyDto Property) : IRequest<Guid>;
}