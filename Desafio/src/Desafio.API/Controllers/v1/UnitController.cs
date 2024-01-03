using Desafio.Application;
using Desafio.Domain;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class UnitController : DesafioControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost]
    public async Task<ActionResult<UnitResponse>> PostUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _unitService.InsertAsync(unitRequest);
        if (result.Success)
            return Ok(result);
        else if (result.Errors.Count > 0)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public async Task<ActionResult<UnitResponse>> GetUnitAsync(string acronym)
    {
        var result = await _unitService.GetByAcronymAsync(acronym.ToUpper());
        
        if (result.Success)
            return Ok(result);
        else if (result.Errors.Count > 0)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut]
    public async Task<ActionResult<UnitResponse>> PutUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _unitService.UpdateAsync(unitRequest);

        if (result.Success)
            return Ok(result);
        else if (result.Errors.Count > 0)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status500InternalServerError);

    }
}
