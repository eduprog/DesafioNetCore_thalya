
using Desafio.Domain;

namespace Desafio.Infrastructure;

public class ProductRepository : IProductRepository
{
    public Task<Product> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
