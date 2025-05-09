using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
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
        RuleForEach(x => x.Items).SetValidator(new SaleItemDtoValidator());        
    }
}
