using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class PersonController : DesafioControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService, IError error) : base(error)
    {
        _personService = personService;
    }

    #region Get
    [HttpGet("get-person-by-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonAsync(Guid id)
    {
        PersonResponse result = await _personService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    [HttpGet("get-all-person")]
    public async Task<ActionResult<PersonResponse>> GetAllPerson()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllAsync();

        return CustomResponseList(result);

    }

    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(string shortId)
    {
        PersonResponse result = await _personService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }

    [HttpGet("get-all-clients")]
    public async Task<ActionResult<PersonResponse>> GetAllClients()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllClientAsync();

        return CustomResponseList(result);

    }

    [HttpGet("get-client-by-id")]
    public async Task<ActionResult<PersonResponse>> GetClientAsync(Guid id)
    {
        PersonResponse result = await _personService.GetClientByIdAsync(id);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-person")]
    public async Task<ActionResult<PersonResponse>> InsertPersonAsync(InsertPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.InsertAsync(personRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-person-information")]
    public async Task<ActionResult<PersonResponse>> UpdatePersonAsync(PersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateAsync(personRequest);

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-enabled-property")]
    public async Task<ActionResult<bool>> UpdatePersonEnabled(EnabledPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateEnablePersonAsync(personRequest);

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-canbuy-property")]
    public async Task<ActionResult<bool>> UpdatePersonSellable(CanBuyPersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateCanBuyPersonAsync(personRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Delete
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-person")]
    public async Task<ActionResult<PersonResponse>> RemovePersonAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.RemoveAsync(id);

        return CustomResponse(result);
    }
    #endregion










   

    
}
