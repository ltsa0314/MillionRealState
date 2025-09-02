using FluentValidation;
using MillionRealState.Application.Features.Properties.Dtos;

public class UpdatePropertyDtoValidator : AbstractValidator<UpdatePropertyDto>
{
    public UpdatePropertyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());

        RuleFor(x => x.CodeInternal)
            .NotEmpty().WithMessage("El código interno es obligatorio.")
            .MaximumLength(50);

        RuleFor(x => x.Year)
            .GreaterThan(0).WithMessage("El año debe ser mayor que cero.");

        RuleFor(x => x.IdOwner)
            .GreaterThan(0).WithMessage("El propietario es obligatorio.");
    }
}