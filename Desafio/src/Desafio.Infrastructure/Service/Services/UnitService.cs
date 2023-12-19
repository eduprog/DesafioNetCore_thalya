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

    public async Task<Unit> GetByIdAsync(int id)
    {
        var unit = await _unitRepository.GetByIdAsync(id);
        return unit;
    }

    public async Task<Unit> InsertAsync(Unit unit)
    {
        await _unitRepository.InsertAsync(unit);
        return unit;
    }

    public async Task RemoveAsync(int id)
    {
        await _unitRepository.RemoveAsync(id);
    }

    public async Task UpdateAsync(Unit unit)
    {
        await _unitRepository.UpdateAsync(unit);
    }
}
