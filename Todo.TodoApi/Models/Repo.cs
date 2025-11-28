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

    public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        var res = await _set.FirstOrDefaultAsync(filter, cancellationToken);
        if (res == null)
        {
            throw new NotFoundException();
        }
        return res;
    }

    public virtual async Task<T> CreateOneAsync(T entity, CancellationToken cancellationToken = default)
    {
        _set.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T updatedEntity, Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        var entity = await GetOneAsync(filter, cancellationToken);
        if(entity == null)
        {
            throw new NotFoundException();
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
            throw new NotFoundException();
        }
        _set.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
