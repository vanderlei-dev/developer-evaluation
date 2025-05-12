using Ambev.DeveloperEvaluation.Domain.Common;
using System.Runtime.CompilerServices;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale: BaseEntity
{
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;      
    }

    public string Number { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public decimal Total { get; set; }
    public bool Cancelled { get; set; }

    public List<SaleItem> Items { get; set; } = [];

    /// <summary>
    /// Calculates the price of each <see cref="Sale.Items"/> and sets <see cref="Sale.Total"/> value.    
    /// </summary>
    /// <remarks>
    /// Discount tiers and item status (cancelled or not) are considered to calculate the total.
    /// </remarks>
    public void CalculateTotal()
    {
        Total = 0;
        foreach (var item in Items)
        {
            item.Discount = CalculateDiscount(item.Quantity);

            item.Total = item.Quantity * (item.UnitPrice - item.UnitPrice * item.Discount);

            if (!item.Cancelled)
                Total += item.Total;
        }

        static decimal CalculateDiscount(int quantity) => quantity switch
        {
            < 4 => 0,
            >= 4 and < 10 => (decimal)0.1,
            >= 10 and <= 20 => (decimal)0.2,
            > 20 => throw new ArgumentOutOfRangeException(nameof(quantity), "Invalid quantity, maximum allowed: 20")
        };        
    }

    /// <summary>
    /// Checks if all <see cref="Sale.Items"/> in the sale are cancelled and sets the <see cref="Sale.Cancelled"/> property accordingly.
    /// </summary>
    public void CheckCancelledItems()
    {
        Cancelled = Items.All(x => x.Cancelled);
    }
}
