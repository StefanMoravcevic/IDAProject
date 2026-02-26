using System.Linq.Expressions;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace IDAProject.Web.Api.Repositories.QueryableExtension
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedResultAsync<T>(this IQueryable<T> query, BasePagedSearchParams pagedParams)
        {
            query = ApplySorting(query, pagedParams.SortBy, pagedParams.SortDirection);

            var result = new PagedList<T>
            {
                PageNumber = pagedParams.PageNumber,
                PageSize = pagedParams.PageSize,
                TotalRowCount = await query.CountAsync()
            };

            query = query.Skip((pagedParams.PageNumber - 1) * pagedParams.PageSize)
                .Take(pagedParams.PageSize);
            
            result.Items = await query.ToListAsync();

            return result;
        }

        private static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortBy, string sortDirection)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortBy);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = sortDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { query.ElementType, property.Type }, query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
