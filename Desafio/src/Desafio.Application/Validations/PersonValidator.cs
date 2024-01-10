using Desafio.Domain;
using Desafio.Identity;
using FluentValidation;

namespace Desafio.Application;

public class PersonValidator : AbstractValidator<Person>
{
    private readonly IPersonService _personService;

    public PersonValidator(IPersonService personService)
    {
        _personService = personService;

        RuleFor(x => x.AlternativeCode)
                .MustAsync(UniqueAlternativeCodeAsync).WithMessage("The Alternative Code must be unique.");
        RuleFor(x => x.Document)
            .MustAsync(UniqueDocumentAsync).WithMessage("The Document must be unique.")
            .MustAsync(IsValid).WithMessage("The Document must be between 11 and 14 caracteres and be numeric only.");

    }
    private async Task<bool> UniqueAlternativeCodeAsync(string alternativeCode, CancellationToken token)
    {
        //Verificar se existe o código alternativo sendo usado em outro cadastro
        return !await _personService.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    private async Task<bool> UniqueDocumentAsync(string document, CancellationToken token)
    {
        //Verificar se existe o documento alternativo sendo usado em outro cadastro
        return !await _personService.DocumentAlreadyExistsAsync(document);
    }
    private async Task<bool> IsValid(string document, CancellationToken token)
    {
        // Verificar se o valor digitado é valido (não tem valores repetidos)
        return _personService.IsValidDocument(document, canBeNullOrEmpty: true);
    }
}
