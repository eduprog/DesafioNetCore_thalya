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

    #region Get
    [HttpGet("get-by-acronym")]
    public async Task<ActionResult<UnitResponse>> GetUnitAsync(string acronym)
    {
        UnitResponse result = await _unitService.GetByAcronymAsync(acronym);

        return CustomResponse(result);

    }

    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<UnitResponse>> GetUnitByShortIdAsync(string shortId)
    {
        UnitResponse result = await _unitService.GetByShortIdAsync(shortId);

        return CustomResponse(result);

    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<UnitResponse>>> GetAllUnitSAsync()
    {
        IEnumerable<UnitResponse> result = await _unitService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No units were found.");

        return CustomResponseList(result);
    }
    #endregion

    #region Post
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-unit")]
    public async Task<ActionResult<UnitResponse>> InsertUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.InsertAsync(unitRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-unit")]
    public async Task<ActionResult<UnitResponse>> UpdateUnitAsync(UnitRequest unitRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.UpdateAsync(unitRequest);

        return CustomResponse(result);

    }
    #endregion

    #region Delete
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-unit")]
    public async Task<ActionResult<UnitResponse>> DeleteUnitAsync(string acronym)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        UnitResponse result = await _unitService.RemoveAsync(acronym.ToUpper());

        return CustomResponse(result);
    }
    #endregion
}
