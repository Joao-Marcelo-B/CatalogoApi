using Catalogo.Api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<T>(items, count, pageNumber, pageSize);
    }
}
