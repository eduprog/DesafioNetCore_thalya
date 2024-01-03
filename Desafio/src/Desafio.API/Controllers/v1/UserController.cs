using Desafio.Application;
using Desafio.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class UserController : DesafioControllerBase
{
    private IIdentityService _identityService;

    public UserController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultado = await _identityService.RegisterUserAsync(registerUserRequest);
        if (resultado.Success)
            return Ok(resultado);

            
        else if (resultado.Errors.Count  > 0) 
            return BadRequest(resultado);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if(!ModelState.IsValid) 
            return BadRequest();

        var resultado = await _identityService.LoginAsync(loginUserRequest);
        if(resultado.Success)
            return Ok(resultado);

        return Unauthorized(resultado);
    }
}
