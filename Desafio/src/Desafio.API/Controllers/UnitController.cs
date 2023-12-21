using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpPost]
    public async Task<Guid> PostUnitAsync(Unit unit)
    {
        Unit newUnit = await _unitService.InsertAsync(unit);
        Guid idUnit = newUnit.Id;
        return idUnit;
    }
    [HttpGet]
    public async Task<Unit> GetUnitAsync(string acronym)
    {
        return await _unitService.GetByAcronymAsync(acronym.ToUpper());
    }
    [HttpPut]
    public async Task<string> PutUnitAsync(Unit unit)
    {
        Unit putUnit = await _unitService.GetByAcronymAsync(unit.Acronym.ToUpper());
        _unitService.UpdateAsync(putUnit);
        return "Editado com Sucesso";
    }
}
