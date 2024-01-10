using Desafio.Domain;

namespace Desafio.Application;

public interface IUnitRepository
{
    Task InsertAsync(Unit product);
    Task<Unit> UpdateAsync(Unit product);
    Task RemoveAsync(string acronym);
    Task<Unit> GetByAcronymAsync(string acronym);
    Task<Unit> GetByShortIdAsync(string shortId);
    Task<List<Unit>> GetAllAsync();
    Task<int> SaveChangesAsync();
    Task<bool> HasNotBeenUsedBeforeAsync(string acronym);
}
