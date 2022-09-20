﻿using LittleByte.Common.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Common.Database.Extensions
{
    public static class QueryableExtension
    {
        public static async Task<PageResponse<T>> GetPagedAsync<T>(this IQueryable<T> queryable, int pageSize, int page)
        {
            var totalResults = await queryable.CountAsync();
            var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalResults / pageSize);

            return new PageResponse<T>(pageSize, page, totalPages, totalResults, results);
        }

        public static async Task<PageResponse<T>> GetPagedAsync<T>(this Task<IQueryable<T>> queryableTask, int pageSize, int page)
        {
            var queryable = await queryableTask;
            return await queryable.GetPagedAsync(pageSize, page);
        }
    }
}