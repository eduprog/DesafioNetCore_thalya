using Desafio.Domain;

namespace Desafio.Application;

public interface IPersonRepository
{
    Task InsertAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task RemoveAsync(Guid id);
    Task<Person> GetByIdAsync(Guid id);
    Task<List<Person>> GetAllAsync();
    Task<int> SaveChangesAsync();
    Task<Person> GetByShortIdAsync(string shortId);
}
