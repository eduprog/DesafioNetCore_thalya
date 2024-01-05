using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class UserValidator : AbstractValidator<User>
{
    private readonly IUserService _userService;

    public UserValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Must(UniqueAcronym).WithMessage("The Email must be unique");
    }

    private bool UniqueAcronym(string acronym)
    {
        // Verificar se existe cadastro dessa unidade
        return !_userService.EmailAlreadyExisists(acronym);
    }
}
