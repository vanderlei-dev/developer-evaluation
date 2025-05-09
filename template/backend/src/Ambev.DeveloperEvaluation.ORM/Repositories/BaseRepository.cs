using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IRepository using Entity Framework Core
/// </summary>
public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BaseRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BaseRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new entity in the database
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Updates a entity in the database
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Retrieves a entity by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a entity from the database
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Set<T>().Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
