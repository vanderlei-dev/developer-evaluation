using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleCommand(SaleDto saleDto) : IRequest<SaleDto>
{
    public SaleDto SaleDto { get; } = saleDto;
}
