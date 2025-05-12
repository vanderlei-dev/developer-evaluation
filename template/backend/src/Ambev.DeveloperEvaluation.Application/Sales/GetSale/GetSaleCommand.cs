using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Represents a request to get a sale from the system.
/// </summary>
public class GetSaleCommand(Guid Id) : IRequest<SaleDto>
{
    public Guid Id { get; } = Id;
}
