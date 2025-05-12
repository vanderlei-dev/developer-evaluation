using Ambev.DeveloperEvaluation.Application.Sales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Validator;

/// <summary>
/// Contains unit tests for the <see cref="SaleItemDtoValidator"/> class.
/// </summary>
public class SaleItemDtoValidatorTests
{
    private SaleItemDtoValidator _validator;

    public SaleItemDtoValidatorTests()
    {
        _validator = new SaleItemDtoValidator();
    }

    [Fact]
    public void Validate_InvalidQuantity_ReturnsError()
    {
        var dto =
                new SaleItemDto()
                {
                    UnitPrice = 1,
                    ProductId = Guid.NewGuid(),
                    Quantity = 21,
                    Discount = (decimal)0.20                    
                };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Validate_InvalidDiscountForQuantityLessThanFour_ReturnsError()
    {
        var dto =
                new SaleItemDto()
                {
                    UnitPrice = 1,
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                    Discount = (decimal)0.10
                };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Discount);
    }

    [Fact]
    public void Validate_InvalidDiscountForQuantityGreaterOrEqualsToFour_ReturnsError()
    {
        var dto =
                new SaleItemDto()
                {
                    UnitPrice = 1,
                    ProductId = Guid.NewGuid(),
                    Quantity = 4,
                    Discount = (decimal)0.5
                };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Discount);
    }

    [Fact]
    public void Validate_InvalidDiscountForQuantityGreaterOrEqualsToTen_ReturnsError()
    {
        var dto =
                new SaleItemDto()
                {
                    UnitPrice = 1,
                    ProductId = Guid.NewGuid(),
                    Quantity = 10,
                    Discount = (decimal)0.1
                };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Discount);
    }
}
