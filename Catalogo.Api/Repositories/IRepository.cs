using System.Linq.Expressions;

namespace Catalogo.Api.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAsQueryable();
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}