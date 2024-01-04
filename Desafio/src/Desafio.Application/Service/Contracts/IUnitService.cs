using Desafio.Domain;

namespace Desafio.Application;
public interface IUnitService
{
    Task<UnitResponse> InsertAsync(UnitRequest unitRequest);
    Task<UnitResponse> UpdateAsync(UnitRequest unitRequest);
    Task RemoveAsync(string acronym);
    Task<UnitResponse> GetByAcronymAsync(string acronym);
    Task<List<Unit>> GetAllAsync();
}
