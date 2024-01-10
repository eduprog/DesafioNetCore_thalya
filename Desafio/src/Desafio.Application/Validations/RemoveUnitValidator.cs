using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class RemoveUnitValidator : AbstractValidator<Unit>
{
    private readonly IUnitService _unitService;

    public RemoveUnitValidator(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.Acronym)
                .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
                .MustAsync(HasBeenUsedBeforeAsync).WithMessage("It's not possible to remove a unit that is being used in a product.")
                .Length(2, 4)
                .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

        return;
    }
    
    private async Task<bool> HasBeenUsedBeforeAsync(string acronym, CancellationToken token)
    {
        //Verifica se a unidade já foi utilizada em um produto
        return !await _unitService.HasBeenUsedBeforeAsync(acronym);
    }
}
