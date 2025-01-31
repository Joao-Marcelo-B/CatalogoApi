﻿using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static PagedList<T> ToPageList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    //public async static Task<PagedList<T>> ToPageListAsync(IQueryable<T> source, int pageNumber, int pageSize)
    //{
    //    var count = await source.CountAsync();
    //    var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

    //    return new PagedList<T>(items, count, pageNumber, pageSize);
    //}
}
