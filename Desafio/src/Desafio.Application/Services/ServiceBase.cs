using Desafio.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Desafio.Application;

public abstract class ServiceBase
{
    private readonly IError _error;

    protected ServiceBase(IError error)
    {
        _error = error;
    }

    protected void Notificate(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificate(error.ErrorMessage);
        }
    }

    protected void Notificate(IEnumerable<IdentityError> identityError)
    {
        foreach (var error in identityError)
        {
            Notificate(error.Description);
        }
    }

    protected void Notificate(string mensagem)
    {
        _error.Handle(new ErrorMessage(mensagem));
    }

    protected async Task<bool> ExecuteValidationAsync<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = await validacao.ValidateAsync(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }

    protected async Task<bool> ExecuteValidationIdentityAsync<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : IdentityUser
    {
        var validator = await validacao.ValidateAsync(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }

    protected string OnlyDocumentNumbers(string document)
    {
        var onlyNumber = "";
        foreach (var value in document)
        {
            if (char.IsDigit(value))
            {
                onlyNumber += value;
            }
        }
        return onlyNumber.Trim();
    }

    protected bool HasRepeatedValues(string document)
    {
        string[] invalidNumbers =
        {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999",
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
        return invalidNumbers.Contains(document);
    }
}