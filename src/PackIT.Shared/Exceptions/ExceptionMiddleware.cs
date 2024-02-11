using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PackIT.Shared.Abstractions.Exceptions;
using System.Net;
using System.Text.Json;

namespace PackIT.Shared.Exceptions;
internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (PackItException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
       
            var errorCode = ToUnderscoreCase(e.GetType().Name.Replace("Exception", string.Empty));
            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, e.Message });
            await context.Response.WriteAsync(json);
        }

    }

    public static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) ? $"_{x}" : x.ToString())).ToLower();
}