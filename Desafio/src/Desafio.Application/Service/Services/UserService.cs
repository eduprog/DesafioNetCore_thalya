using Desafio.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio.Identity;

public class UserService : IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public UserService(SignInManager<IdentityUser> signInManager, 
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
            return await GenerateToken(loginUserRequest.Email);

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

    public async Task<LoginUserResponse> LoginWithoutPassword(string usuarioId)
    {
        var userLoginResponse = new LoginUserResponse();
        var user = await _userManager.FindByIdAsync(usuarioId);

        if (await _userManager.IsLockedOutAsync(user))
            userLoginResponse.InsertError("This user is blocked");
        else if (!await _userManager.IsEmailConfirmedAsync(user))
            userLoginResponse.InsertError("This user must confirm the e-mail to continue");

        if (userLoginResponse.Success)
            return await GenerateCredentials(user.Email);

        return userLoginResponse;
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

        string roleDescription = registerUserRequest.Role.ToString();
        var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, roleDescription);

        RegisterUserResponse userRegisterResponse = new RegisterUserResponse(result.Succeeded);
        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisterResponse.InsertErrors(result.Errors.Select(x => x.Description));

        return userRegisterResponse;
    }

    private async Task<LoginUserResponse> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jwt ID
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //criação
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //emissão

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Sender,
            Audience = _jwtOptions.ValidIn,
            Subject = identityClaims,
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

    private async Task<LoginUserResponse> GenerateCredentials(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jwt ID
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //criação
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //emissão

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Sender,
            Audience = _jwtOptions.ValidIn,
            Subject = identityClaims,
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

    //Converter corretamente os segundos da data
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
