using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio.Identity;

public class UserService : ServiceBase, IUserService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly JwtOptions _jwtOptions;
    private readonly IMapper _mapper;
    private readonly IError _error;

    public UserService(SignInManager<User> signInManager, 
                           UserManager<User> userManager,
                           IOptions<JwtOptions> jwtOptions,
                           IMapper mapper,
                           IError error) : base (error)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _mapper = mapper;
        _error = error;
    } 

    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, true);
        if (result.Succeeded) return await GenerateToken(loginUserRequest.Email);

        return null;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
    {
        var identityUser = _mapper.Map<User>(registerUserRequest);
        identityUser.EmailConfirmed = true;

        var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);

        if (!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                Notificate(error.Description);
            }
            return null;
        }

        //desbloquear usuário já que não terá e-mail de confirmação
        await _userManager.SetLockoutEnabledAsync(identityUser, false);

        string roleDescription = registerUserRequest.UserLevel.ToString();
        await _userManager.AddToRoleAsync(identityUser, roleDescription);

        RegisterUserResponse userRegisterResponse = _mapper.Map<RegisterUserResponse>(identityUser);

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
            Email = email,
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
