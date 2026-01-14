using MediatR;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Commands;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.CommandHandlers
{
    /// <summary>
    /// Handler para cambiar el precio de una propiedad.
    /// </summary>
    public class ChangePropertyPriceCommandHandler : IRequestHandler<ChangePropertyPriceCommand>
    {
        private readonly IPropertyReadRepository _readRepo;
        private readonly IPropertyWriteRepository _writeRepo;
        private readonly IValidator<ChangePriceDto> _validator;

        public ChangePropertyPriceCommandHandler(
            IPropertyReadRepository readRepo,
            IPropertyWriteRepository writeRepo,
            IValidator<ChangePriceDto> validator)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _validator = validator;
        }

        public async Task Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Price, cancellationToken);

            var property = await _readRepo.GetByIdAsync(request.IdProperty);
            if (property == null)
                throw new KeyNotFoundException($"Property {request.IdProperty} no existe.");

            property.ChangePrice(request.Price.NewPrice);

            await _writeRepo.UpdateAsync(property);
        }
    }
}   