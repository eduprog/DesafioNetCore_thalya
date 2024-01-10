using Desafio.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Desafio.API;

[ApiController]
[Route("[controller]")]
public abstract class DesafioControllerBase : ControllerBase
{
    protected readonly IError _error;

    protected DesafioControllerBase(IError error)
    {
        _error = error;
    }

    protected ActionResult CustomResponse(object result = null, string message = null)
    {
        if (IsValid())
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                return Ok(new
                {
                    sucess = true,
                    data = message
                });
            }

            return Ok(new
            {
                sucess = true,
                data = result
            });
        }

        return BadRequest(new
        {
            sucess = false,
            errors = _error.GetErrors().Select(x => x.Error)
        });
    }
    protected ActionResult CustomResponseList<T>(IEnumerable<T> result, string message = null)
    {
        if (IsValid())
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                return Ok(new
                {
                    sucess = true,
                    data = message
                });
            }

            return Ok(new
            {
                sucess = true,
                data = result
            });
        }

        return BadRequest(new
        {
            sucess = false,
            errors = _error.GetErrors().Select(x => x.Error)
        });
    }
    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if(!ModelState.IsValid) NotificateErrorInvalidModel(modelState);
        
        return CustomResponse();
    }

    protected void NotificateErrorInvalidModel(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(x => x.Errors);

        foreach(var error in errors)
        {
            var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

            NotifyErrors(errorMessage);
        }
    }
    protected void NotifyErrors (string message)
    {
        _error.Handle(new ErrorMessage(message));
    }
    protected bool IsValid()
    {
        return !_error.HasError();
    }
}
