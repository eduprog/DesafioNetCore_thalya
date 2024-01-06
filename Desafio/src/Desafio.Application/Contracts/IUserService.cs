namespace Desafio.Application;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<UserResponse>> GetAllAsync(bool selectRoles);
    Task<IEnumerable<UserResponse>> GetAllUsersByRoleAsync(string role);
    bool EmailAlreadyExisists(string email);
    bool DocumentAlreadyExisists(string document);
    bool IsValidDocument(string document);
}
