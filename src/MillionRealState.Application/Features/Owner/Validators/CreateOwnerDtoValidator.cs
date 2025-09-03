using FluentValidation;
using MillionRealState.Application.Features.Owner.Dtos;

public class CreateOwnerDtoValidator : AbstractValidator<CreateOwnerDto>
{
    public CreateOwnerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Address)
            .SetValidator(new AddressDtoValidator());

        RuleFor(x => x.Birthday)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de nacimiento no puede ser futura.");

        RuleFor(x => x.Photo)
            .MaximumLength(250);
    }
}