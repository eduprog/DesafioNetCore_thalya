using Desafio.Domain;

namespace Desafio.Application;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<UnitResponse> GetAllAsync()
    {
        var units = await _unitRepository.GetAllAsync();

        if (units != null)
        {
            var unitResponse = new UnitResponse
            {
                Success = true
            };
            unitResponse.InsertUnits(units);

            return unitResponse;
        }
        else
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError("The unit was not found.");

            return unitResponse;
        }
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
        try
        {
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
        catch (Exception ex) 
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError(ex.Message);

            return unitResponse;
        }
        //verifica se unidade já existe
        
    }

    public async Task<UnitResponse> RemoveAsync(string acronym)
    {
        try
        {
            if (_unitRepository.HasBeenUsedBefore(acronym))
            {
                UnitResponse unitResponse = new UnitResponse(false);
                unitResponse.InsertError("It's not possible to remove a unit that is being used in a product.");

                return unitResponse;
            }
            else
            {
                await _unitRepository.RemoveAsync(acronym);

                var unitResponse = new UnitResponse
                {
                    Success = true
                };

                return unitResponse;
            }
            

        }
        catch (Exception ex)
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError(ex.Message);

            return unitResponse;
        }
    }

    public async Task<UnitResponse> UpdateAsync(UnitRequest unitRequest)
    {
        try
        {
            //verifica se unidade já existe
            var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

            if (existingUnit != null)
            {
                existingUnit.Acronym = unitRequest.Acronym.ToUpper();
                existingUnit.Description = unitRequest.Description.ToUpper();

                await _unitRepository.UpdateAsync(existingUnit);

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
        catch (Exception ex)
        {
            UnitResponse unitResponse = new UnitResponse(false);
            unitResponse.InsertError(ex.Message);

            return unitResponse;
        }
    }
}
