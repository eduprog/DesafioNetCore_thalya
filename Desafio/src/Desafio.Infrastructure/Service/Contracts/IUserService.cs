using Desafio.Domain;

namespace Desafio.Infrastructure;

public interface IUserService
{
    Task InsertAsync(User product);
    Task UpdateAsync(User product);
    Task RemoveAsync(int id);
    Task<User> GetByIdAsync(int id);
    Task<User> GetAllAsync();
}
