using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class UnitValidator : AbstractValidator<Unit>
{
    private readonly IUnitService _unitService;

    public UnitValidator(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.Acronym)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UnitDoesNotExistsAsync).WithMessage("The Acronym must be unique.")
            .Length(2, 4).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

        RuleFor(x => x.Description)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");
    }
    

    private async Task<bool> UnitDoesNotExistsAsync(string acronym, CancellationToken token)
    {
        // Verificar se existe cadastro dessa unidade
        return !await _unitService.UnitDoesNotExistsAsync(acronym);
    }
}
