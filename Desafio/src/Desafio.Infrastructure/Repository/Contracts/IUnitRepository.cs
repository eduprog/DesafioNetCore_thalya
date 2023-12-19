using Desafio.Domain;

namespace Desafio.Infrastructure;

public interface IUnitRepository
{
    Task InsertAsync(Unit product);
    Task UpdateAsync(Unit product);
    Task RemoveAsync(int id);
    Task<Unit> GetByIdAsync(int id);
    Task<List<Unit>> GetAllAsync();
}
