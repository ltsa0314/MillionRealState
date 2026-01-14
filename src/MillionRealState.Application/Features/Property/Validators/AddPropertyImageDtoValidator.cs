using FluentValidation;
using MillionRealState.Application.Features.Property.Dtos;

public class AddPropertyImageDtoValidator : AbstractValidator<AddPropertyImageDto>
{
    public AddPropertyImageDtoValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty().WithMessage("El archivo de imagen es obligatorio.")
            .MaximumLength(250);

        RuleFor(x => x.Enabled)
            .NotNull().WithMessage("El estado de la imagen es obligatorio.");
    }
}