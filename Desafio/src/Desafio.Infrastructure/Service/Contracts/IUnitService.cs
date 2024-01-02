using Desafio.Application;
using Desafio.Domain;

namespace Desafio.Infrastructure;
public interface IUnitService
{
    Task<UnitResponse> InsertAsync(UnitRequest unit);
    void UpdateAsync(Unit product);
    Task RemoveAsync(string acronym);
    Task<Unit> GetByAcronymAsync(string acronym);
    Task<List<Unit>> GetAllAsync();
}
