using Desafio.Domain;

namespace Desafio.Application;

public interface IPersonRepository
{
    Task InsertAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task RemoveAsync(Guid id);
    Task<Person> GetByIdAsync(Guid id);
    Task<Person> GetClientByIdAsync(Guid id);
    Task<List<Person>> GetAllAsync();
    Task<List<Person>> GetAllClientAsync();
    Task<int> SaveChangesAsync();
    Task<Person> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> PersonCanBuyAsync(Guid id);
}
