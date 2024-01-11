using Desafio.Domain;
using Desafio.Identity;
using FluentValidation;
using System.Reflection.Metadata;
using System.Xml.Linq;

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
            .MustAsync(UniqueDocumentAsync).WithMessage("The Document must be unique.");

        RuleFor(x => x.Document).IsValidCNPJ().Unless(x => x.Document.Equals("") || x.Document.Length <= 11);
        RuleFor(x => x.Document).IsValidCPF().Unless(x => x.Document.Equals("") || x.Document.Length > 11);

    }
    private async Task<bool> UniqueAlternativeCodeAsync(string alternativeCode, CancellationToken token)
    {
        //Verificar se existe o código alternativo sendo usado em outro cadastro
        if (string.IsNullOrWhiteSpace(alternativeCode)) return true;
        return !await _personService.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    private async Task<bool> UniqueDocumentAsync(string document, CancellationToken token)
    {
        //Verificar se existe o documento alternativo sendo usado em outro cadastro

        if (string.IsNullOrWhiteSpace(document)) return true;
        return !await _personService.DocumentAlreadyExistsAsync(document);
    }
}
