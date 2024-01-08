using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class AccessPermitionsController : DesafioControllerBase
{
    private IUserService _userService;

    public AccessPermitionsController(IUserService identityService, IError error) : base(error)
    {
        _userService = identityService;
    }

    [HttpGet("get-all-users-roles")]
    public async Task<ActionResult<UserResponse>> GetAllUsersRoles()
    {
        var result = await _userService.GetAllAsync(true);

        if (result.Count() == 0) return CustomResponse(result, "No users were found.");

        return CustomResponse(result);
    }
}
