using Desafio.Domain;

namespace Desafio.Infrastructure;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        var getUnits = await _unitRepository.GetAllAsync();
        return getUnits;
    }

    public async Task<Unit> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym);
        return unit;
    }

    public async Task<Unit> InsertAsync(Unit unit)
    {
        unit.Id = Guid.NewGuid();
        await _unitRepository.InsertAsync(unit);
        return unit;
    }

    public async Task RemoveAsync(string acronym)
    {
        await _unitRepository.RemoveAsync(acronym);
    }

    public void UpdateAsync(Unit unit)
    {
        _unitRepository.UpdateAsync(unit);
    }
}
