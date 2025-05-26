using Microsoft.AspNetCore.Diagnostics;
using Shared.Models;

namespace Application.Handlers;

public class ExceptionHandler  : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ResponseModel<ErrorResponse> errorResponseModel = new()
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Something went wrong"
        };
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(errorResponseModel, cancellationToken);
        return true;
    }
}