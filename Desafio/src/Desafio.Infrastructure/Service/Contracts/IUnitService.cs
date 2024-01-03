using Desafio.Application;
using Desafio.Domain;

namespace Desafio.Infrastructure;
public interface IUnitService
{
    Task<UnitResponse> InsertAsync(UnitRequest unitRequest);
    Task<UnitResponse> UpdateAsync(UnitRequest unitRequest);
    Task RemoveAsync(string acronym);
    Task<UnitResponse> GetByAcronymAsync(string acronym);
    Task<List<UnitResponse>> GetAllAsync();
}
