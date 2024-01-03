using Desafio.Application;
using Desafio.Domain;
using Desafio.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio.Identity;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(SignInManager<IdentityUser> signInManager, 
                           UserManager<IdentityUser> userManager,
                           IOptions<JwtOptions> jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, true);
        if (result.Succeeded)
            return GenerateToken(loginUserRequest.Email);

        LoginUserResponse loginUserResponse = new LoginUserResponse(result.Succeeded);

        List<string> errors = new List<string>();
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                errors.Add("E-mail is blocked.");
            else if (result.IsNotAllowed)
                errors.Add("Permission Denied.");
            else if (result.RequiresTwoFactor)
                errors.Add("Confirm your login in your second confirmation factor.");
            else
                errors.Add("Incorrect Login or Password.");

            loginUserResponse.InsertErrors(errors);
        }

        return loginUserResponse;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        IdentityUser identityUser = new ()
        {
            UserName = registerUserRequest.Email,
            Email = registerUserRequest.Email,
            //não vai trabalhar com e-mail de confirmação
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);
        if (result.Succeeded)
            //desbloquear usuário já que não terá e-mail de confirmação
            await _userManager.SetLockoutEnabledAsync(identityUser, false);

        string roleDescription = registerUserRequest.Role.GetEnumDescription();
        var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, roleDescription);

        RegisterUserResponse userRegisterResponse = new RegisterUserResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisterResponse.InsertErrors(result.Errors.Select(x => x.Description));

        return userRegisterResponse;
    }

    private LoginUserResponse GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Sender,
            Audience = _jwtOptions.ValidIn,
            Expires = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationHour),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return new LoginUserResponse
        {
            Success = true,
            Token = encodedToken,
            DataExpiration = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationHour)
        }; 
    }
    private async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id)); 
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jwt ID
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString())); //criação
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())); //emissão

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        return claims;
    }
}
