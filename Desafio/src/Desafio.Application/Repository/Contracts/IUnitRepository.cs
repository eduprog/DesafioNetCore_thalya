using Desafio.Domain;

namespace Desafio.Application;

public interface IUnitRepository
{
    Task InsertAsync(Unit product);
    Task<Unit> UpdateAsync(Unit product);
    Task RemoveAsync(string acronym);
    Task<Unit> GetByAcronymAsync(string acronym);
    Task<List<Unit>> GetAllAsync();
    Task<int> SaveChangesAsync();
}
