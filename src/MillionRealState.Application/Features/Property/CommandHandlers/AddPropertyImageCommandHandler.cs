using MediatR;
using AutoMapper;
using FluentValidation;
using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Application.Features.Property.Commands;
using MillionRealState.Application.Features.Property.Dtos;

namespace MillionRealState.Application.Features.Property.CommandHandlers
{
    /// <summary>
    /// Handler para agregar una imagen a una propiedad.
    /// </summary>
    public class AddPropertyImageCommandHandler : IRequestHandler<AddPropertyImageCommand>
    {
        private readonly IPropertyReadRepository _readRepo;
        private readonly IPropertyWriteRepository _writeRepo;
        private readonly IValidator<AddPropertyImageDto> _validator;

        public AddPropertyImageCommandHandler(
            IPropertyReadRepository readRepo,
            IPropertyWriteRepository writeRepo,
            IValidator<AddPropertyImageDto> validator)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _validator = validator;
        }

        public async Task Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Image, cancellationToken);

            var property = await _readRepo.GetByIdAsync(request.IdProperty);
            if (property == null)
                throw new KeyNotFoundException($"Property {request.IdProperty} no existe.");

            property.AddImage(request.Image.File, request.Image.Enabled);

            await _writeRepo.UpdateAsync(property);
        }
    }
}   