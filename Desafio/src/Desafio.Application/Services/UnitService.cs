using AutoMapper;
using Desafio.Domain;
using System.Collections.Generic;

namespace Desafio.Application;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;
    private readonly IError _error;

    public UnitService(IUnitRepository unitRepository, IMapper mapper, IError error)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
        _error = error;
    }

    public async Task<IEnumerable<UnitResponse>> GetAllAsync()
    {
        var units = _mapper.Map< IEnumerable<UnitResponse>>(await _unitRepository.GetAllAsync());

        if (units != null)
        {

            var unitResponse = new List<UnitResponse>();

            return unitResponse;
        }
        else
        {
            var unitResponse = new List<UnitResponse>();
            //unitResponse.InsertError("The unit was not found.");

            return unitResponse;
        }
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
        if (await UnitAlreadyExists(unitRequest.Acronym.ToUpper()))
        {
            _error.Handle(new ErrorMessage("The Acronym already exists."));
            return null;
        }

        var unit = _mapper.Map<Unit>(unitRequest);
        await _unitRepository.InsertAsync(unit);

        return null;

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
    public async Task<bool> UnitAlreadyExists(string acronym)
    {
        return await _unitRepository.GetByAcronymAsync(acronym) != null;
    }
}
