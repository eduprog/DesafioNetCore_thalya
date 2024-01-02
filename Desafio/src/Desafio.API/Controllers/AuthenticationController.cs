using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API;

[Route("api")]
public class AuthenticationController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Register(RegisterUserRequest registerUser)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(registerUser);
        }

        var user = new IdentityUser()
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(registerUser);
        }

        return Ok(registerUser);
    }

    [HttpPost("entrar")]
    public async Task<ActionResult> Login(LoginUserRequest loginUser)
    {
        if (!ModelState.IsValid) return BadRequest(loginUser);

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, isPersistent: false, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return Ok(loginUser);
        }
        if (result.IsLockedOut)
        {
            return BadRequest(new
            {
                message = "Usuario temporariamente bloqueado",
                data = loginUser
            });
        }
        return BadRequest(new
        {
            message = "Usuario ou senha incorretos",
            data = loginUser
        });
    }
}
