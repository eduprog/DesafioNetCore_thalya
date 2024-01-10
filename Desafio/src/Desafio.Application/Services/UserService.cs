using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
    #region Controller Methods
    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest)
    {
        //usar mapper
        var result = await _signInManager.PasswordSignInAsync(loginUserRequest.Email, loginUserRequest.Password, false, true);
        if (result.Succeeded) return await GenerateToken(loginUserRequest.Email);

        Notificate("Incorrect email or password.");
        return null;
    }

    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, string authenticatedUser)
    {
        //verificar se existe usuário na base
        if(!HasAnyUserRegisteredOnDatabase()  && registerUserRequest.UserLevel != EUserLevel.Administrator)
        {
            Notificate("There must be at least one administrator user before entering any other information on the database.");
            return null;
        }

        if (string.IsNullOrWhiteSpace(authenticatedUser))
        {
            Notificate("No user is authenticated.");
            return null;
        }

        if(ReturnUserRole(authenticatedUser) != "ADMINISTRATOR")
        {
            Notificate("Only administrator user can register another user.");
            return null;
        }

        var identityUser = _mapper.Map<User>(registerUserRequest);

        identityUser.EmailConfirmed = true;
        identityUser.Document = OnlyDocumentNumbers(registerUserRequest.Document);

        if (!await ExecuteValidationIdentityAsync(new UserValidator(this), identityUser))
        {
            return null;
        }

        identityUser.ShortId = GenerateShortId("USER");
        var result = await _userManager.CreateAsync(identityUser, registerUserRequest.Password);

        if (!result.Succeeded)
        {
            Notificate(result.Errors);
            return null;
        }

        //desbloquear usuário já que não terá e-mail de confirmação
        await _userManager.SetLockoutEnabledAsync(identityUser, false);

        string roleDescription = registerUserRequest.UserLevel.ToString();
        await _userManager.AddToRoleAsync(identityUser, roleDescription);

        RegisterUserResponse userRegisterResponse = _mapper.Map<RegisterUserResponse>(identityUser);

        return userRegisterResponse;
    }

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
            ShortId = user.ShortId,
            Roles = selectRoles ? await _userManager.GetRolesAsync(user).ConfigureAwait(true) : null
        });

        var mappedUsers = await Task.WhenAll(usersRoles);
        return mappedUsers;
    }

    public async Task<UserResponse> GetByShortIdAsync(string shortId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.ShortId == shortId);

        if(user == null)
        {
            Notificate("No user was found.");
            return null;
        }
        
        UserResponse userResponse = _mapper.Map<UserResponse>(user);
        userResponse.Roles = _userManager.GetRolesAsync(user).Result;

        return userResponse;
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
            ShortId = user.ShortId,
            Roles = _userManager.GetRolesAsync(user).Result
        });

        return usersRoles;
    }
    public async Task<UserResponse> UpdateAsync(UpdateUserRequest userRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);
        

        if (existingUser == null)
        {
            Notificate("The user was not found.");
            return null;
        }

        var existingRole = await _userManager.GetRolesAsync(existingUser);

        existingUser.Document = userRequest.Document ?? existingUser.Document;
        existingUser.Name = userRequest.Name ?? existingUser.Name;
        existingUser.UserName = userRequest.UserName ?? existingUser.UserName;
        existingUser.Email = userRequest.Email;
        existingUser.NickName = userRequest.NickName ?? existingUser.NickName;

        await _userManager.UpdateAsync(existingUser);

        string newRole = userRequest.UserLevel.ToString();
        await _userManager.RemoveFromRoleAsync(existingUser, existingRole.FirstOrDefault());
        await _userManager.AddToRoleAsync(existingUser, newRole);

        var userResponse = _mapper.Map<UserResponse>(existingUser);

        userResponse.Roles.Add(newRole);

        return userResponse;

    }
    public async Task<UserResponse> UpdateAsync (UpdateLoginUserRequest userRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(userRequest.Email);

        if (existingUser == null)
        {
            Notificate("The user was not found.");
            return null;
        }

        var result = await _userManager.ChangePasswordAsync(existingUser, userRequest.CurrentPassword, userRequest.NewPassword);
        if(result.Succeeded) 
        {
            var userResponse = _mapper.Map<UserResponse>(existingUser);

            return userResponse;
        }

        Notificate(result.Errors);
        return null;
        
    }

    public async Task<UserResponse> RemoveAsync(string email)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if(existingUser == null)
        {
            Notificate("Email was not found.");
            return null;
        }
        await _userManager.DeleteAsync(existingUser);

        return null;
    }
    #endregion

    #region Helper Methods 
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
            DataExpiration = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationHour),
            ShortId = user.ShortId
        };
    }

    //Converter corretamente os segundos da data
    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
    #endregion

    #region Validations Methods
    public async Task<bool> EmailAlreadyExisistsAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<bool> DocumentAlreadyExisistsAsync(string document)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Document == document) != null;
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

    public bool HasAnyUserRegisteredOnDatabase()
    {
        return _userManager.GetUsersInRoleAsync("ADMINISTRATOR").Result.Count > 0;
    }

    private string ReturnUserRole(string id)
    {
        User user = _userManager.Users.FirstOrDefault(x => x.Id == id);

        return _userManager.GetRolesAsync(user).Result.ToList().FirstOrDefault();
    }
    #endregion
}
