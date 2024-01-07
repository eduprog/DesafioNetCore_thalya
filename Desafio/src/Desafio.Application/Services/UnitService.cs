using AutoMapper;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Desafio.Application;

public class UnitService : ServiceBase, IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public UnitService(IUnitRepository unitRepository, IMapper mapper, IError error) : base(error)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnitResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<UnitResponse>>(await _unitRepository.GetAllAsync());

        return result;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym);

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<UnitResponse> InsertAsync(UnitRequest unitRequest)
    {
        var unit = _mapper.Map<Unit>(unitRequest);

        if (!ExecuteValidation(new UnitValidator(this), unit)) 
        {
            return null;
        }

        unit.ShortId = GenerateShortId("UNIT");

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
                Notificate("It's not possible to remove a unit that is being used in a product.");
                return null;
            }
            else
            {
                await _unitRepository.RemoveAsync(acronym);

                return null;
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
                var unit = _mapper.Map<Unit>(unitRequest);

                existingUnit.Acronym = unitRequest.Acronym.ToUpper();
                existingUnit.Description = unitRequest.Description.ToUpper();

                await _unitRepository.UpdateAsync(existingUnit);

                var unitResponse = _mapper.Map<UnitResponse>(existingUnit);

                return unitResponse;
            }
            else
            {
                Notificate("The unit was not found.");
                return null;
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
        return _unitRepository.IsRegistered(acronym);
    }
}
