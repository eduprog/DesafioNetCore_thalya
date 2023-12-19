using Desafio.Domain;

namespace Desafio.Infrastructure;
public interface IUnitService
{
    Task<Unit> InsertAsync(Unit product);
    void UpdateAsync(Unit product);
    Task RemoveAsync(string acronym);
    Task<Unit> GetByAcronymAsync(string acronym);
    Task<List<Unit>> GetAllAsync();
}
