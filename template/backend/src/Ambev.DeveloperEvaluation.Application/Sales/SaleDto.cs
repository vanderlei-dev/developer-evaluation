namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Represents a sale data transfer object.
/// </summary>
public record SaleDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Number { get; set; } = null!;        
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public decimal Total { get; set; }
    public bool Cancelled { get; set; }
    public List<SaleItemDto> Items { get; set; } = [];    
}
