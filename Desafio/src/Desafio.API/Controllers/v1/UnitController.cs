using Desafio.Application;
using Desafio.Domain;
using Desafio.Identity;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

[Authorize]
public class UnitController : DesafioControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpPost]
    public async Task<ActionResult<UnitResponse>> PostUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultado = await _unitService.InsertAsync(unitRequest);
        if (resultado.Success)
            return Ok(resultado);
        else if (resultado.Errors.Count > 0)
            return BadRequest(resultado);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
    //[HttpGet]
    //public async Task<Unit> GetUnitAsync(string acronym)
    //{
    //    return await _unitService.GetByAcronymAsync(acronym.ToUpper());
    //}
    //[HttpPut]
    //public async Task<string> PutUnitAsync(Unit unit)
    //{
    //    Unit putUnit = await _unitService.GetByAcronymAsync(unit.Acronym.ToUpper());
    //    _unitService.UpdateAsync(putUnit);
    //    return "Editado com Sucesso";
    //}
}
