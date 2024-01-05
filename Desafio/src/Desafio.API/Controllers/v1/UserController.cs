using Desafio.Application;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class UserController : DesafioControllerBase
{
    private IUserService _userService;

    public UserController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        if(!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }
        var resultado = await _userService.RegisterUserAsync(registerUserRequest);
        
        return CustomResponse(resultado);   
    }
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);

        var resultado = await _userService.LoginAsync(loginUserRequest);

        return CustomResponse(resultado);
    }

    //implementar exclusão, edição e listagem das permissões
}
