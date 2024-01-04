namespace Desafio.Application;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
}
