using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Commands
{
    /// <summary>
    /// Comando para agregar una imagen a una propiedad.
    /// </summary>
    public record AddPropertyImageCommand(Guid IdProperty, AddPropertyImageDto Image) : IRequest;
}   