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
            .Must(UniqueEmail).WithMessage("The Email must be unique.");
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Must(UniqueDocument).WithMessage("The Document must be unique.")
            .Must(ValidLenght).WithMessage("The Document must be between 11 and 14 caracteres and be numeric only.");
    }

    private bool UniqueEmail(string email)
    {
        // Verificar se existe cadastro desse e-mail
        return !_userService.EmailAlreadyExisists(email);
    }
    private bool UniqueDocument(string document)
    {
        // Verificar se existe cadastro desse documento
        return !_userService.DocumentAlreadyExisists(document);
    }
    private bool ValidLenght(string document)
    {
        // Verificar se o valor digitado é valido (não tem valores repetidos)
        return _userService.IsValidDocument(document);
    }
}
