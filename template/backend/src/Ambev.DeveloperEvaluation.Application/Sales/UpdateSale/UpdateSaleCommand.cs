using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents a request to update a sale from the system.
/// </summary>
public class UpdateSaleCommand(SaleDto saleDto) : IRequest<SaleDto>
{
    public SaleDto SaleDto { get; } = saleDto;
}
