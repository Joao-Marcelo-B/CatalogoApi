using System.Linq.Expressions;

namespace Catalogo.Api.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    IQueryable<T> Get();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}