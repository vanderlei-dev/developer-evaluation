using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core.
/// </summary>
public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context) : base(context)
    {

    }

    /// <summary>
    /// Retrieves a sale together with all related items.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale if found, null otherwise.</returns>
    public async Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
                        .Sales
                        .Include(x => x.Items)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}
