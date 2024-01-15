using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Desafio.API;

public class AuthorizationController : DesafioControllerBase
{
    private IUserService _userService;

    public AuthorizationController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    #region Post
    [HttpPost("register-user")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        RegisterUserResponse result = await _userService.RegisterUserAsync(registerUserRequest, User);

        return CustomResponse(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        LoginUserResponse result = await _userService.LoginAsync(loginUserRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Get 
    [HttpGet("get-all-users-roles")]
    public async Task<ActionResult<UserResponse>> GetAllUsersRoles()
    {
        var result = await _userService.GetAllAsync(true);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }
    #endregion
}
