namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Represents a sale item data transfer object.
/// </summary>
public record SaleItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Total { get; set; }
    public bool? Cancelled { get; set; }
}