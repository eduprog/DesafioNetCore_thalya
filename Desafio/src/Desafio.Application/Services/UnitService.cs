using AutoMapper;
using Desafio.Domain;

namespace Desafio.Application;

public class UnitService : ServiceBase, IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;
    private readonly IError _error;

    public UnitService(IUnitRepository unitRepository, IMapper mapper, IError error) : base(error)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
        _error = error;
    }

    public async Task<IEnumerable<UnitResponse>> GetAllAsync()
    {
        var units = _mapper.Map< IEnumerable<UnitResponse>>(await _unitRepository.GetAllAsync());

        return units;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym);

        if(unit != null)
        {
            var unitResponse = new UnitResponse();
            
            return unitResponse;
        }
        else
        {
            UnitResponse unitResponse = new UnitResponse();
            //unitResponse.InsertError("The unit was not found.");

            return unitResponse;
        }
    }

    public async Task<UnitResponse> InsertAsync(UnitRequest unitRequest)
    {
        var unit = _mapper.Map<Unit>(unitRequest);

        if (!ExecuteValidation(new UnitValidation(this), unit)) 
        {
            return null;
        }
        
        await _unitRepository.InsertAsync(unit);
        var newUnit = _mapper.Map<UnitResponse>(unit);
        return newUnit;

    }

    public async Task<UnitResponse> RemoveAsync(string acronym)
    {
        try
        {
            if (_unitRepository.HasBeenUsedBefore(acronym))
            {
                UnitResponse unitResponse = new UnitResponse();
                //unitResponse.InsertError("It's not possible to remove a unit that is being used in a product.");

                return unitResponse;
            }
            else
            {
                await _unitRepository.RemoveAsync(acronym);

                var unitResponse = new UnitResponse();

                return unitResponse;
            }
            

        }
        catch (Exception ex)
        {
            UnitResponse unitResponse = new UnitResponse();
            //unitResponse.InsertError(ex.Message);

            return unitResponse;
        }
    }

    public async Task<UnitResponse> UpdateAsync(UnitRequest unitRequest)
    {
        try
        {
            var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

            if (existingUnit != null)
            {
                existingUnit.Acronym = unitRequest.Acronym.ToUpper();
                existingUnit.Description = unitRequest.Description.ToUpper();

                await _unitRepository.UpdateAsync(existingUnit);

                var unitResponse = new UnitResponse();

                return unitResponse;
            }
            else
            {
                UnitResponse unitResponse = new UnitResponse();
                //unitResponse.InsertError("The unit was not found.");

                return unitResponse;
            }
        }
        catch (Exception ex)
        {
            UnitResponse unitResponse = new UnitResponse();
            //unitResponse.InsertError(ex.Message);

            return unitResponse;
        }
    }
    public bool UnitAlreadyExists(string acronym)
    {
        var unitExists = _unitRepository.IsRegistered(acronym);
        return unitExists;
    }
}
