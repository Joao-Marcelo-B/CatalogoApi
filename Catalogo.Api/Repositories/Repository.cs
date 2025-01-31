using Catalogo.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalogo.Api.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CatalogoContext _context;

    public Repository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public IQueryable<T> GetAsQueryable()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        //_context.SaveChanges();

        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        //_context.SaveChanges();

        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();

        return entity;
    }
}
