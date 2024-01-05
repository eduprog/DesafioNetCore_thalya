using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class UnitController : DesafioControllerBase
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService, IError error) : base(error)
    {
        _unitService = unitService;
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost]
    public async Task<ActionResult<UnitResponse>> PostUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _unitService.InsertAsync(unitRequest);

        return CustomResponse(ModelState);
    }

    //[HttpGet("get-by-acronym")]
    //public async Task<ActionResult<UnitResponse>> GetUnitAsync(string acronym)
    //{
    //    var result = await _unitService.GetByAcronymAsync(acronym.ToUpper());

    //    return CustomResponse(ModelState);

    //    //return StatusCode(StatusCodes.Status500InternalServerError);
    //}

    //[HttpGet("get-all")]
    //public async Task<IEnumerable<UnitResponse>> GetAllUnitSAsync()
    //{
    //    var unit = await _unitService.GetAllAsync();

    //    return unit;

    //    //return StatusCode(StatusCodes.Status500InternalServerError);

    //}

    //[Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    //[HttpPut]
    //public async Task<ActionResult<UnitResponse>> PutUnitAsync(UnitRequest unitRequest)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var result = await _unitService.UpdateAsync(unitRequest);

    //    return CustomResponse(ModelState);

    //    //return StatusCode(StatusCodes.Status500InternalServerError);

    //}
    //[Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    //[HttpDelete]
    //public async Task<ActionResult<UnitResponse>> DeleteUnitAsync(string acronym)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var result = await _unitService.RemoveAsync(acronym.ToUpper());

    //    return CustomResponse(ModelState);

    //    //return StatusCode(StatusCodes.Status500InternalServerError);

    //}
}
