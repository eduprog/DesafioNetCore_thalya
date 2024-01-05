namespace Desafio.Application;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<IEnumerable<UserResponse>> GetAllUsersByRoleAsync(string role);
    bool EmailAlreadyExisists(string email);
}
