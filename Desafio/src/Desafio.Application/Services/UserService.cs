using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
    #region Insert and Login   
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

        if (!ExecuteValidationIdentity(new UserValidator(this), identityUser))
        {
            return null;
        }

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
    #endregion

    public async Task<IEnumerable<UserResponse>> GetAllAsync(bool selectRoles)
    {
        IEnumerable<User> users = await _userManager.Users.ToListAsync();

        var usersRoles = users.Select(async user => new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Document = user.Document,
            Name = user.Name,
            NickName = user.NickName,
            UserName = user.Name,
            Roles = selectRoles ? await _userManager.GetRolesAsync(user).ConfigureAwait(true) : null
        });

        var mappedUsers = await Task.WhenAll(usersRoles);
        return mappedUsers;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersByRoleAsync(string role)
    {
        IEnumerable<User> users = await _userManager.GetUsersInRoleAsync(role);

        var usersRoles = users.Select(user => new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Document = user.Document,
            Name = user.Name,
            NickName = user.NickName,
            UserName = user.Name,
            Roles = _userManager.GetRolesAsync(user).Result
        });

        return usersRoles;
    }


    public bool EmailAlreadyExisists(string email)
    {
        return _userManager.Users.FirstOrDefault(x => x.Email == email) != null;
    }
    public bool DocumentAlreadyExisists(string document)
    {
        return _userManager.Users.FirstOrDefault(x => x.Document == document) != null;
    }
    public bool IsValidDocument(string document)
    {
        string documentNumber = OnlyDocumentNumbers(document);
        bool validLength = documentNumber.Length == 11 || documentNumber.Length == 14;

        if (string.IsNullOrWhiteSpace(documentNumber) || HasRepeatedValues(documentNumber) || !validLength)
        {
            return false;
        }

        return true;

    }
    public static string OnlyDocumentNumbers(string document)
    {
        var onlyNumber = "";
        foreach (var value in document)
        {
            if (char.IsDigit(value))
            {
                onlyNumber += value;
            }
        }
        return onlyNumber.Trim();
    }
    private static bool HasRepeatedValues(string document)
    {
        string[] invalidNumbers =
        {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999",
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
        return invalidNumbers.Contains(document);
    }
    
}
