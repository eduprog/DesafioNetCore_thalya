using AutoMapper;
using Desafio.Domain;

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

    #region Controller Methods
    public async Task<IEnumerable<UnitResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<UnitResponse>>(await _unitRepository.GetAllAsync());

        return result;
    }

    public async Task<UnitResponse> GetByAcronymAsync(string acronym)
    {
        var unit = await _unitRepository.GetByAcronymAsync(acronym.ToUpper());

        if (unit == null)
        {
            Notificate("The unit was not found.");
            return null;
        }

        return _mapper.Map<UnitResponse>(unit);
    }

    public async Task<UnitResponse> GetByShortIdAsync(string shortId)
    {
        var unit = await _unitRepository.GetByShortIdAsync(shortId);

        if (unit == null)
        {
            Notificate("No units were found.");
            return null;
        }

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
        var unit = await _unitRepository.GetByAcronymAsync(acronym);
        if (unit == null)
        {
            Notificate("The unit was not found");
            return null;
        }

        if (!ExecuteValidation(new UnitValidator(this, removeUnit: true), unit))
        {
            return null;
        }

        await _unitRepository.RemoveAsync(acronym);

        return null;
    }

    public async Task<UnitResponse> UpdateAsync(UnitRequest unitRequest)
    {
        var existingUnit = await _unitRepository.GetByAcronymAsync(unitRequest.Acronym.ToUpper());

        if (existingUnit != null)
        {
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
    #endregion

    #region Validations Methods
    public bool UnitAlreadyExists(string acronym)
    {
        return _unitRepository.IsRegistered(acronym);
    }
    public bool HasBeenUsedBefore(string acronym)
    {
        return _unitRepository.HasBeenUsedBefore(acronym);
    }
    #endregion
}
