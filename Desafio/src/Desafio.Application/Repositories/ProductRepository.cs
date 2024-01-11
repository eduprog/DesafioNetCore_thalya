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
        return await _appDbContext.Products.AsNoTracking().ToListAsync();
    }
    public async Task<List<Product>> GetAllSellableAsync()
    {
        return await _appDbContext.Products.AsNoTracking().Where(x => x.Sellable).ToListAsync();
    }

    public async Task<Product> GetByBarCodeAsync(string barCode)
    {
        return await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.BarCode == barCode);
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Product product)
    {
        try
        {
            //product.Unit = _appDbContext.Units.AsNoTracking().FirstOrDefault(x => x.Acronym == product.Acronym);
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

    public async Task<Product> GetByShortIdAsync(string shortId)
    {
        return await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ShortId == shortId);
    }

    public async Task<bool> UnitAlreadyExistsAsync(string acronym)
    {
        return await _appDbContext.Units.AsNoTracking().FirstOrDefaultAsync(x => x.Acronym == acronym) != null;
    }
}
