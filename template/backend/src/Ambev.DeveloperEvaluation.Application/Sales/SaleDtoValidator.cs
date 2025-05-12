using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Validator for <see cref="SaleDto"/> that defines validation rules for sale creation and modification.
/// </summary>
public class SaleDtoValidator : AbstractValidator<SaleDto>
{
    /// <summary>
    /// Initializes a new instance of the SaleDtoValidator with defined validation rules.
    /// </summary>    
    public SaleDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.Items)
            .Must(x => x.GroupBy(i => i.ProductId).All(g => g.Count() == 1))
            .WithMessage("Sale items should be unique.");

        RuleForEach(x => x.Items).SetValidator(new SaleItemDtoValidator());        
    }
}
