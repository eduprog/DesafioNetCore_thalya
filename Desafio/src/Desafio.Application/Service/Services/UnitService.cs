using Desafio.Domain;

namespace Desafio.Application;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<List<UnitResponse>> GetAllAsync()
    {
        //var getUnits = await _unitRepository.GetAllAsync();
        //return getUnits;
        return null;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym);

        if(unit != null)
        {
            var unitResponse = new UnitResponse
            {
                Success = true,
                Unit = unit,
            };

            return unitResponse;
        }
        else
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError("The unit was not found.");

            return unitResponse;
        }
    }

    public async Task<UnitResponse> InsertAsync(UnitRequest unitRequest)
    {
        //verifica se unidade já existe
        var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

        if (existingUnit == null)
        {
            Unit unit = new Unit();
            unit.Id = Guid.NewGuid();
            unit.Description = unitRequest.Description.ToUpper();
            unit.Acronym = unitRequest.Acronym.ToUpper();

            await _unitRepository.InsertAsync(unit);

            //adicionar verificação de erro

            var unitResponse = new UnitResponse
            {
                Success = true,
                Unit = unit,
            };

            return unitResponse;
        }
        else
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError("The unit already exists.");

            return unitResponse;
        }
    }

    public async Task RemoveAsync(string acronym)
    {
        await _unitRepository.RemoveAsync(acronym);
    }

    public async Task<UnitResponse> UpdateAsync(UnitRequest unitRequest)
    {
        //verifica se unidade já existe
        var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

        if (existingUnit != null)
        {
            _unitRepository.UpdateAsync(existingUnit);

            //adicionar verificação de erro

            var unitResponse = new UnitResponse
            {
                Success = true,
                Unit = existingUnit,
            };

            return unitResponse;
        }
        else
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError("The unit was not found.");

            return unitResponse;
        }
    }
}
