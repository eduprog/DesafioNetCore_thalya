using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class RemovePersonValidator : AbstractValidator<Person>
{
    private readonly IPersonService _personService;

    public RemovePersonValidator(IPersonService personService)
    {
        _personService = personService;

        RuleFor(x => x.Id)
                .MustAsync(VerifyIfPersonCanBuyAsync).WithMessage("Can't remove person that can buy.");
    }
    private async Task<bool> VerifyIfPersonCanBuyAsync(Guid id, CancellationToken token)
    {
        //Verificar se a pessoa está permitida para comprar
        var teste = await _personService.PersonCanBuyAsync(id);
        return !await _personService.PersonCanBuyAsync(id);
    }
}
