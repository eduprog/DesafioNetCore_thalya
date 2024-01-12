using Desafio.Domain;
using Microsoft.AspNetCore.Http;
using System.Net;

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

            switch (exception)
            {
                case CustomException e:
                    errorResult.StatusCode = (int)e.StatusCode;
                    if(e.ErrorMessages is not null) 
                    {
                        errorResult.Messages = e.ErrorMessages;
                    }
                break;

                default:
                    errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
            }

        }
    }
}
