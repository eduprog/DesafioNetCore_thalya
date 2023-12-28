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
    public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
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
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
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

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "O campo {0} é obrigatório")]
    public string ConfirmPassword { get; set; }
}
public class LoginUserViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }

}