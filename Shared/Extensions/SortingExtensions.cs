using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Extensions;

public static class SortingExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy, bool isAscending)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            sortBy = "Id";
        }

        var propertyInfo = typeof(T).GetProperty(sortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
            return query;
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, propertyInfo.Name);
        var selector = Expression.Lambda(property, parameter);

        var method = isAscending ? "OrderBy" : "OrderByDescending";

        var result = typeof(Queryable).GetMethods()
            .First(m => m.Name == method && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type)
            .Invoke(null, [query, selector]);

        return (IQueryable<T>)result!;
    }
}