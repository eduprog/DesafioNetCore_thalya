using Desafio.Domain;

namespace Desafio.Application;

public interface IProductRepository
{
    Task InsertAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task RemoveAsync(Guid id);
    Task<Product> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllSalableAsync();
    Task<int> SaveChangesAsync();
}
