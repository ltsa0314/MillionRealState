using FluentValidation;
using MillionRealState.Application.Features.Properties.Dtos;

public class ChangePriceDtoValidator : AbstractValidator<ChangePriceDto>
{
    public ChangePriceDtoValidator()
    {
        RuleFor(x => x.NewPrice)
            .GreaterThan(0).WithMessage("El nuevo precio debe ser mayor que cero.");
    }
}