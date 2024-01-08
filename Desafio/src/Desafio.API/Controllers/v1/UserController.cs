using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Authorization;
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
        var result = await _userService.GetAllAsync(false);
        
        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-all-users-roles")]
    public async Task<ActionResult<UserResponse>> GetAllUsersRoles()
    {
        var result = await _userService.GetAllAsync(true);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }
    [HttpGet("get-all-administrator-users")]
    public async Task<ActionResult<UserResponse>> GetAllAdministratorUsers()
    {
        var result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Administrator.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No administrator users were found.");

        return CustomResponse(result);
    }

    [HttpGet("get-all-manager-users")]
    public async Task<ActionResult<UserResponse>> GetAllManagerUsers()
    {
        var result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Manager.ToString().ToUpper());
        
        if(result.Count() ==  0) return CustomResponse(result, "No manager users were found.");
        
        return CustomResponse(result);
    }

    [HttpGet("get-all-seller-users")]
    public async Task<ActionResult<UserResponse>> GetAllSellerUsers()
    {
        var result = await _userService.GetAllUsersByRoleAsync(EUserLevel.Seller.ToString().ToUpper());

        if (result.Count() == 0) return CustomResponse(result, "No seller users were found.");

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut]
    public async Task<ActionResult<UserResponse>> PutUserAsync(UserRequest userRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UserResponse result = await _userService.UpdateAsync(userRequest);

        return CustomResponse(result);

    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete]
    public async Task<ActionResult<UserResponse>> DeleteUserAsync(string email)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _userService.RemoveAsync(email);

        return CustomResponse(result);

    }
}
