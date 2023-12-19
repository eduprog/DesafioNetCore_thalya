using Desafio.Domain;

namespace Desafio.Application;
public interface IUnitService
{
    Task InsertAsync(Unit product);
    Task UpdateAsync(Unit product);
    Task RemoveAsync(int id);
    Task<Unit> GetByIdAsync(int id);
    Task<Unit> GetAllAsync();
}
