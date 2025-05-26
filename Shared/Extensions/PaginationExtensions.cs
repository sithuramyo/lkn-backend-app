using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.Extensions;

public static class PaginationExtensions
{
    public static async Task<PaginationResponse<T>> ToPagedListAsync<T>(
        this IQueryable<T> query, PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query.Skip(request.Skip).Take(request.PageSize).ToListAsync(cancellationToken);

        return new PaginationResponse<T>
        {
            Items = data,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}