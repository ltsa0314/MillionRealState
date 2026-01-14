using MediatR;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.Commands
{
    /// <summary>
    /// Comando para cambiar el precio de una propiedad.
    /// </summary>
    public record ChangePropertyPriceCommand(Guid IdProperty, ChangePriceDto Price) : IRequest;
}   