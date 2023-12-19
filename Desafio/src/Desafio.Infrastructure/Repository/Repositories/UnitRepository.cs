using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Desafio.Infrastructure;

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
        await _appDbContext.Units.AddAsync(unit);
        await SaveChangesAsync();
    }

    public async Task RemoveAsync(string acronym)
    {
        Unit unit = await GetByAcronymAsync(acronym);
        _appDbContext.Units.Remove(unit);
        await SaveChangesAsync();
    }

    public async void UpdateAsync(Unit unit)
    {
        _appDbContext.Update(unit);
        await SaveChangesAsync();
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}
