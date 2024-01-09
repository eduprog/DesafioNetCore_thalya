using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class UnitValidator : AbstractValidator<Unit>
{
    private readonly IUnitService _unitService;

    public UnitValidator(IUnitService unitService, bool removeUnit = false)
    {
        _unitService = unitService;

        if (removeUnit)
        {
            RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(HasBeenUsedBeforeAsync).WithMessage("It's not possible to remove a unit that is being used in a product.")
            .Length(2, 4)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

            return;
        }

        RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueAcronymAsync).WithMessage("The Acronym must be unique.")
            .Length(2, 4)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");
    }
    

    private async Task<bool> UniqueAcronymAsync(string acronym, CancellationToken token)
    {
        // Verificar se existe cadastro dessa unidade
        return !await _unitService.UnitAlreadyExistsAsync(acronym);
    }
    private async Task<bool> HasBeenUsedBeforeAsync(string acronym, CancellationToken token)
    {
        return !await _unitService.HasBeenUsedBeforeAsync(acronym);
    }
}
