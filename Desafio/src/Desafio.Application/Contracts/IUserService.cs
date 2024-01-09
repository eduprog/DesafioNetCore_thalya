namespace Desafio.Application;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUserRequest);
    Task<IEnumerable<UserResponse>> GetAllAsync(bool selectRoles);
    Task<IEnumerable<UserResponse>> GetAllUsersByRoleAsync(string role);
    Task<UserResponse> GetByShortIdAsync(string shortId);
    Task<UserResponse> UpdateAsync(UpdateUserRequest userRequest);
    Task<UserResponse> UpdateAsync(UpdateLoginUserRequest userRequest);
    Task<UserResponse> RemoveAsync(string email);
    Task<bool> EmailAlreadyExisistsAsync(string email);
    Task<bool> DocumentAlreadyExisistsAsync(string document);
    bool IsValidDocument(string document);
}
