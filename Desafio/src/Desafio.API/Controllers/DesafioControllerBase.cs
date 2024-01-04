using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Desafio.API;

[ApiController]
[Route("[controller]")]
public abstract class DesafioControllerBase : ControllerBase
{
    protected ICollection<string> Errors { get; set; } = new List<string>();
    protected ActionResult CustomResponse(object result = null)
    {
        if (IsValid())
        {
            return Ok(new
            {
                sucess = true,
                data = result
            });
        }

        return BadRequest(new
        {
            sucess = false,
            errors = Errors.ToList()
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
        Errors.Add(message);
    }
    public bool IsValid()
    {
        return !Errors.Any();
    }
}
