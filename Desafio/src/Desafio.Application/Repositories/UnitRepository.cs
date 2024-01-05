using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application;

public class UnitRepository : IUnitRepository
{

    private readonly AppDbContext _appDbContext;
    
    public UnitRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        return await _appDbContext.Units.ToListAsync();
    }

    public async Task<Unit> GetByAcronymAsync(string acronym)
    {
        return await _appDbContext.Units.FirstOrDefaultAsync(x => x.Acronym == acronym);
    }

    public async Task InsertAsync(Unit unit)
    {
        try
        {
            unit.Id = Guid.NewGuid();
            await _appDbContext.Units.AddAsync(unit);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("Error while inserting unit");
        }
        
    }

    public async Task RemoveAsync(string acronym)
    {
        try
        {
            Unit unit = await GetByAcronymAsync(acronym);
            if(unit == null)
            {
                throw new Exception($"Unit {acronym} doesn't exists.");
            }
            _appDbContext.Units.Remove(unit);
            await SaveChangesAsync();
        }
        catch (Exception)
        {
            throw; 
        }
        
    }

    public async Task<Unit> UpdateAsync(Unit unit)
    {
        try
        {
            _appDbContext.Update(unit);
            await SaveChangesAsync();
            return unit;
        }
        catch (Exception)
        { 
            throw new Exception("Error while updating unit");
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
            throw new Exception("Error while saving unit");
        }
        
    }

    public bool HasBeenUsedBefore(string acronym)
    {
        return _appDbContext.Products.Any(x => x.Acronym == acronym);
    }
    public bool IsRegistered(string acronym)
    {
        return _appDbContext.Units.Any(x => x.Acronym == acronym);
    }
}
