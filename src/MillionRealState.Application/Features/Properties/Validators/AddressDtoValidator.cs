using FluentValidation;
using MillionRealState.Application.Features.Properties.Dtos;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("La calle es obligatoria.")
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es obligatoria.")
            .MaximumLength(50);

        RuleFor(x => x.State)
            .MaximumLength(50);

        RuleFor(x => x.ZipCode)
            .MaximumLength(20);

        RuleFor(x => x.Country)
            .MaximumLength(50);
    }
}