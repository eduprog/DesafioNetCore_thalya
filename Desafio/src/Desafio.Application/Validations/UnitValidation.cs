using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class UnitValidation : AbstractValidator<Unit>
{
    private readonly IUnitService _unitService;

    public UnitValidation(IUnitService unitService)
    {
        _unitService = unitService;

        RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Must(UniqueAcronym).WithMessage("The Acronym must be unique")
            .Length(2, 4)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");
    }

    private bool UniqueAcronym(string acronym)
    {
        // Verificar se existe cadastro dessa unidade
        var teste = !_unitService.UnitAlreadyExists(acronym);
        return teste;
    }
}
