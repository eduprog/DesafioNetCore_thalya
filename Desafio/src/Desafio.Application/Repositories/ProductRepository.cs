using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }
    public async Task<List<Product>> GetAllSalableAsync()
    {
        return await _appDbContext.Products.Where(x => x.Salable).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Product product)
    {
        try
        {
            product.Id = Guid.NewGuid();
            product.Unit = _appDbContext.Units.FirstOrDefault();
            await _appDbContext.Products.AddAsync(product);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("Error while inserting product");
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        try
        {
            Product product = await GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product {id} doesn't exists.");
            }
            _appDbContext.Products.Remove(product);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _appDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("Error while saving product");
        }
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        try
        {
            _appDbContext.Update(product);
            await SaveChangesAsync();
            return product;
        }
        catch (Exception)
        {
            throw new Exception("Error while updating product");
        }
    }
}
