using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
{
    /// <summary>
    /// Initializes a new instance of the SaleItemDtoValidator with defined validation rules.
    /// </summary>  
    public SaleItemDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .LessThanOrEqualTo(20)
            .WithMessage("Invalid quantity, maximum allowed: 20");

        RuleFor(x => x.Discount)
            .Must(x => x == 0)
                .WithMessage("Invalid discount for quantity less than 4 items.")
                .When(x => x.Discount != null && x.Quantity < 4, ApplyConditionTo.CurrentValidator)
            .Must(x => x == (decimal)0.1)
                .WithMessage("Invalid discount for quantity between 4 and 10 items. Discount should be 10%.")
                .When(x => x.Discount != null && x.Quantity >= 4 && x.Quantity < 10, ApplyConditionTo.CurrentValidator)
            .Must(x => x == (decimal)0.2)
                .WithMessage("Invalid discount for quantity greater or equals to 10 items. Dicsount should be 20%.")
                .When(x => x.Discount != null && x.Quantity >= 10, ApplyConditionTo.CurrentValidator);                
    }
}