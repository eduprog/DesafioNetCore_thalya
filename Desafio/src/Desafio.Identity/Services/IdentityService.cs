using Desafio.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            return await GenerateTokenAsync(loginUserRequest.Email);

        LoginUserResponse loginUserResponse = new LoginUserResponse(result.Succeeded);
        if(!result.Succeeded)
        {
            if (result.IsLockedOut)
                loginUserResponse.InsertError("E-mail is blocked.");
            else if (result.IsNotAllowed)
                loginUserResponse.InsertError("Permission Denied.");
            else if (result.RequiresTwoFactor)
                loginUserResponse.InsertError("Confirm your login in your second confirmation factor.");
            else
                loginUserResponse.InsertError("Incorrect Login or Password.");
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

        RegisterUserResponse userRegisterResponse = new RegisterUserResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisterResponse.InsertErrors(result.Errors.Select(x => x.Description));

        return userRegisterResponse;
    }
    private async Task<LoginUserResponse> GenerateTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var tokenClaims = await GetClaimsAsync(user);

        var dataExpiration = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            //claims: tokenClaims,
            notBefore: DateTime.Now,
            expires: dataExpiration,
            signingCredentials: _jwtOptions.SigningCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new LoginUserResponse
        {
            Success = true,
            Token = token,
            DataExpiration = dataExpiration
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
