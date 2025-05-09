using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T user, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T user, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);    
}
