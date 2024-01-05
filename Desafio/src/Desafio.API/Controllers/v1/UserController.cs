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
        var result = await _userService.RegisterUserAsync(registerUserRequest);
        
        return CustomResponse(result);   
    }
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _userService.LoginAsync(loginUserRequest);

        return CustomResponse(result);
    }

    [HttpGet("get-all-users")]
    public async Task<ActionResult<UserResponse>> GetAllUsers()
    {
        var result = await _userService.GetAllAsync();

        return CustomResponse(result);
    }
    //implementar exclusão, edição e listagem das permissões
}
