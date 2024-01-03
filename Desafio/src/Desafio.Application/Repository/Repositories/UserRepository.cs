using Desafio.Domain;

namespace Desafio.Application;

internal class UserRepository : IUserRepository
{
    public Task<User> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(User product)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User product)
    {
        throw new NotImplementedException();
    }
}

