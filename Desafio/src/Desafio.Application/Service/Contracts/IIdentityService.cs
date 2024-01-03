namespace Desafio.Application;

public interface IIdentityService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
}
