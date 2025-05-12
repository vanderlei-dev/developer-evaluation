using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests the discount calculation based on the quantity of items in the sale.
    /// </summary>    
    [Theory]
    [InlineData(3, 0)]
    [InlineData(4, 0.1)]
    [InlineData(10, 0.2)]
    public void Should_CalculateDiscount_WhenQuantityIsValid(int quantity, decimal expectedDiscount)
    {
        // Arrange
        var sale = new Sale()
        {
            Items = new List<SaleItem>()
            {
                new SaleItem()
                {
                    Quantity = quantity
                }
            }
        };

        // Act
        sale.CalculateTotal();

        // Assert
        Assert.Equal(sale.Items[0].Discount, expectedDiscount);
    }

    /// <summary>
    /// Tests the total calculation of the sale based on the items and their quantities, discount and status.
    /// </summary>
    [Fact]
    public void Should_CalculateTotal_WhenQuantityIsValid()
    {
        // Arrange
        var sale = new Sale()
        {
            Items = new List<SaleItem>()
            {
                new SaleItem()
                {
                    Quantity = 4,
                    UnitPrice = 1,
                    Discount = (decimal)0.1,
                    Cancelled = true
                },
                new SaleItem()
                {
                    Quantity = 4,
                    UnitPrice = 1,
                    Discount = (decimal)0.1
                }
            }
        };

        // Act
        sale.CalculateTotal();

        // Assert
        Assert.Equal((decimal)3.60, sale.Items[0].Total);
        Assert.Equal((decimal)3.60, sale.Items[1].Total);
        Assert.Equal((decimal)3.60, sale.Total);
    }

    /// <summary>
    /// Tests if <see cref="Sale.CalculateTotal"/> throws exception when quantity is invalid.
    /// </summary>
    [Fact]
    public void Should_ThrowException_WhenQuantityIsInvalid()
    {
        // Arrange
        var sale = new Sale()
        {
            Items = new List<SaleItem>()
            {
                new SaleItem()
                {
                    Quantity = 21,
                    UnitPrice = 1,
                    Discount = (decimal)0.2
                }
            }
        };

        // Act
        Action calculateTotal = sale.CalculateTotal;

        // Assert        
        Assert.Throws<ArgumentOutOfRangeException>(calculateTotal);
    }
}
