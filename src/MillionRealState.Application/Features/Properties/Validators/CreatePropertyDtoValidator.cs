using FluentValidation;
using MillionRealState.Application.Features.Properties.Dtos;

namespace MillionRealState.Application.Features.Properties.Validators
{
    public sealed class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage("El código interno es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Year)
                .GreaterThan(0).WithMessage("El año debe ser válido.");

            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El propietario es obligatorio.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("La dirección es obligatoria.");

            RuleFor(x => x.Address.City)
                .NotEmpty().WithMessage("La ciudad no puede estar vacía.");
        }
    }

}
