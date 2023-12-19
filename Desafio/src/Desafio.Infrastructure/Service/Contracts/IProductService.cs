using Desafio.Domain;

namespace Desafio.Infrastructure;
public interface IProductService
{
    Task InsertAsync(Product product);
    Task UpdateAsync(Product product);
    Task RemoveAsync(int id);
    Task<Product> GetByIdAsync(int id);
    Task<Product> GetAllAsync();
}

