using Ambev.DeveloperEvaluation.Application.Sales;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Contains test data for Sale test cases.
/// </summary>
public static class SaleTestData
{    
    private static Faker<SaleDto> GetSaleDtoFaker(bool cancelled)
    {
        return new Faker<SaleDto>()
                .RuleFor(x => x.UserId, f => f.Random.Guid())
                .RuleFor(x => x.BranchId, f => f.Random.Guid())
                .RuleFor(x => x.Number, f => f.Random.AlphaNumeric(6))
                .RuleFor(x => x.Items, f => GetSaleItemDtoFaker(cancelled).Generate(5));
    }

    private static Faker<SaleItemDto> GetSaleItemDtoFaker(bool cancelled)
    {
        return new Faker<SaleItemDto>()
              .RuleFor(x => x.ProductId, f => f.Random.Guid())
              .RuleFor(x => x.Quantity, f => f.Random.Int(1, 20))
              .RuleFor(x => x.UnitPrice, f => f.Random.Decimal(1, 200))
              .RuleFor(x => x.Cancelled, f => cancelled);
    }

    public static SaleDto GenerateValidSale(bool cancelled = false)
    {
        return GetSaleDtoFaker(cancelled).Generate();
    }
}
