using FluentValidation;
using MillionRealState.Application.Features.Properties.Dtos;

namespace MillionRealState.Application.Features.Properties.Validators
{
    public sealed class UpdatePropertyDtoValidator : AbstractValidator<UpdatePropertyDto>
    {
        public UpdatePropertyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(200);

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage("El código interno es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.Year)
                .GreaterThan(0);

            RuleFor(x => x.OwnerId)
                .GreaterThan(0);

            RuleFor(x => x.Address)
                .NotNull();
        }
    }
}
