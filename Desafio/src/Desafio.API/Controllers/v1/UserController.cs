using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Desafio.API;

public class UserController : DesafioControllerBase
{
    private IUserService _userService;

    public UserController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    #region Post
    [HttpPost("register-user")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var authenticatedUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        RegisterUserResponse result = await _userService.RegisterUserAsync(registerUserRequest, authenticatedUser);

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
    [HttpGet("get-all-users")]
    public async Task<ActionResult<UserResponse>> GetAllUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllAsync(false);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-all-administrator-users")]
    public async Task<ActionResult<UserResponse>> GetAllAdministratorUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Administrator.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No administrator users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-all-manager-users")]
    public async Task<ActionResult<UserResponse>> GetAllManagerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Manager.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No manager users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-all-seller-users")]
    public async Task<ActionResult<UserResponse>> GetAllSellerUsers()
    {
        IEnumerable<UserResponse> result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Seller.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No seller users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UserResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UserResponse result = await _userService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }
    #endregion

    #region Put
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-user-informations")]
    public async Task<ActionResult<UserResponse>> UpdateUserAsync(UpdateUserRequest userRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.UpdateAsync(userRequest);

        return CustomResponse(result);

    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-login")]
    public async Task<ActionResult<UserResponse>> UpdateLoginUserAsync(UpdateLoginUserRequest userRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.UpdateAsync(userRequest);

        return CustomResponse(result);

    }
    #endregion

    #region Delete
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-user")]
    public async Task<ActionResult<UserResponse>> DeleteUserAsync(string email)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.RemoveAsync(email);

        return CustomResponse(result);

    }
    #endregion
}
