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
            .MustAsync(UniqueEmailAsync).WithMessage("The Email must be unique.");
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueDocument).WithMessage("The Document must be unique.")
            .Must(ValidLenght).WithMessage("The Document must be between 11 and 14 caracteres and be numeric only.");
    }

    private async Task<bool> UniqueEmailAsync(string email, CancellationToken token)
    {
        // Verificar se existe cadastro desse e-mail
        return !await _userService.EmailAlreadyExisistsAsync(email);
    }
    private async Task<bool> UniqueDocument(string document, CancellationToken token)
    {
        // Verificar se existe cadastro desse documento
        return !await _userService.DocumentAlreadyExisistsAsync(document);
    }
    private bool ValidLenght(string document)
    {
        // Verificar se o valor digitado é valido (não tem valores repetidos)
        return  _userService.IsValidDocument(document);
    }
}
