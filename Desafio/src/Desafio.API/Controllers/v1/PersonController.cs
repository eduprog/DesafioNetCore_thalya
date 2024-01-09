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

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost]
    public async Task<ActionResult<PersonResponse>> InsertPersonAsync(InsertPersonRequest personRequest)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.InsertAsync(personRequest);

        return CustomResponse(result);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonAsync(Guid id)
    {
        PersonResponse result = await _personService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<PersonResponse>> GetAllPerson()
    {
        IEnumerable<PersonResponse> result = await _personService.GetAllAsync();

        if (!result.Any()) return CustomResponseList(result, "No person was foud");

        return CustomResponseList(result);

    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut]
    public async Task<ActionResult<PersonResponse>> UpdatePersonAsync(PersonRequest personRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.UpdateAsync(personRequest);

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete]
    public async Task<ActionResult<PersonResponse>> RemovePersonAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        PersonResponse result = await _personService.RemoveAsync(id);

        return CustomResponse(result);
    }

    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<PersonResponse>> GetPersonByShortIdAsync(string shortId)
    {
        PersonResponse result = await _personService.GetByShortIdAsync(shortId);

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
}
