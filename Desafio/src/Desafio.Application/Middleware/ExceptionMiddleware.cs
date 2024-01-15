using Desafio.Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Desafio.Application;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var errorId = Guid.NewGuid().ToString();
            ErrorResult errorResult = new()
            {
                Source = exception.TargetSite?.DeclaringType?.Name,
                Exception = exception.Message,
                ErrorId = errorId
            };

            errorResult.StatusCode = (int)ReturnStatusCode(exception);

           
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            await response.WriteAsync(JsonSerializer.Serialize(errorResult));
        }
    }
    private HttpStatusCode ReturnStatusCode(Exception exception)
    {
        switch (exception)
        {
            case KeyNotFoundException:
                return HttpStatusCode.NotFound;
            case ValidationException:
                return HttpStatusCode.BadRequest;
            case FluentValidation.ValidationException:
                return HttpStatusCode.UnprocessableEntity;
            default:
               return HttpStatusCode.InternalServerError;
        }
    }
}
