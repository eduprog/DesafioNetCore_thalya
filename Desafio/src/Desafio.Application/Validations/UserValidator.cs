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
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueDocument).WithMessage("The Document must be unique.");

        RuleFor(x => x.Document).IsValidCNPJ().Unless(x => x.Document.Length <= 11);
        RuleFor(x => x.Document).IsValidCPF().Unless(x => x.Document.Length > 11);
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
}
