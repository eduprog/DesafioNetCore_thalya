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
            .Must(BeUniqueAcronym).WithMessage("The Acronym must be unique")
            .Length(2, 4)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Length(2, 50)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");
    }

    private bool BeUniqueAcronym(string acronym)
    {
        // Verificar se o acrônimo é único no banco de dados
        var teste= _unitService.UnitAlreadyExists(acronym);
        return !_unitService.UnitAlreadyExists(acronym);
    }
}
