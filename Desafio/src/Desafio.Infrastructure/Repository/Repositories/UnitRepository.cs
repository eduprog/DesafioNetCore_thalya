using Desafio.Domain;

namespace Desafio.Infrastructure;

public class UnitRepository : IUnitRepository
{
    public Task<Unit> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Unit> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Unit product)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Unit product)
    {
        throw new NotImplementedException();
    }
}
