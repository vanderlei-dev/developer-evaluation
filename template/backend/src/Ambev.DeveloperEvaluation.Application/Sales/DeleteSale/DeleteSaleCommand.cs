using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Represents a request to delete a sale from the system.
/// </summary>
public class DeleteSaleCommand(Guid Id) : IRequest<bool>
{
    public Guid Id { get; } = Id;
}
