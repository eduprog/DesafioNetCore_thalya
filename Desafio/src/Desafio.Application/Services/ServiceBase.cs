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

    protected bool ExecuteValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }

    protected async Task<bool> ExecuteValidationAsync<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = await validacao.ValidateAsync(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }

    protected bool ExecuteValidationIdentity<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : IdentityUser
    {
        var validator = validacao.Validate(entidade);

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
    protected string GenerateShortId(string input)
    {
        DateTime date = DateTime.Now;

        string newId = $"{date:yyyyMMddHHmmss}-{input}";

        string idBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(newId));

        string shortId = new string(idBase64
            .Where(x => char.IsLetterOrDigit(x))
            .ToArray());
        shortId = shortId.Substring(shortId.Length - 10);

        return shortId;
    }
}