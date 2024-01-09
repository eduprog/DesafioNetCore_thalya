using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class PersonValidator : AbstractValidator<Person>
{
    private readonly IPersonService _personService;

    public PersonValidator(IPersonService personService)
    {
        _personService = personService;


    }
}
