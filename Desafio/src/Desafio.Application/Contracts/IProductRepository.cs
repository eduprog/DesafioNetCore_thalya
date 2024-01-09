using Desafio.Domain;

namespace Desafio.Application;

public interface IProductRepository
{
    Task InsertAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task RemoveAsync(Guid id);
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> GetByBarCodeAsync(string barCode);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllSalableAsync();
    Task<int> SaveChangesAsync();
    Task<bool> UnitAlreadyExistsAsync(string acronym);
    Task<Product> GetByShortIdAsync(string shortId);
}
