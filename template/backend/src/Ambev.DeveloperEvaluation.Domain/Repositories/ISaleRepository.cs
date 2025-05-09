using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository : IRepository<Sale>
    {
        Task<Sale?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}