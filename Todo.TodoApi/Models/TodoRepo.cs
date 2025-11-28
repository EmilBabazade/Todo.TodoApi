using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Linq.Expressions;
using Todo.TodoApi.DB;

namespace Todo.TodoApi.Models;

public class Repo<T>(DbContext context) where T : class, IUnique
{
    protected readonly DbSet<T> _set = context.Set<T>();
    protected readonly DbContext _context = context;

    public virtual async Task<IEnumerable<T>> GetManyAsync(Func<T, bool>? filter = null, CancellationToken cancellationToken = default)
    {
        if (filter == null)
        {
            return await _set.ToArrayAsync(cancellationToken);
        }
        return await _set.Where(x => filter(x)).ToArrayAsync(cancellationToken);
    }

    public virtual async Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _set.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public virtual async Task<T?> UpdateAsync(T updatedEntity, Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        var entity = await GetOneAsync(filter, cancellationToken);
        if(entity == null)
        {
            throw new NotFoundException("Entity to update doesn't exist");
        }
        entity = updatedEntity;
        _set.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetOneAsync(x => x.Id == id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException("Entity to update doesn't exist");
        }
        _set.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class NotFoundException(string message) : Exception(message)
{
}