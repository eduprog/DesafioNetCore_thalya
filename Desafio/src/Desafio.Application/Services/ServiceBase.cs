using Desafio.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

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
    protected bool ExecuteValidationIdentity<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : IdentityUser
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificate(validator);

        return false;
    }
    protected string GenerateShortId(string input)
    {
        DateTime date = DateTime.Now;

        string newId = $"{date:yyyyMMddHHmmss}-{input}";

        byte[] bytesBase64 = System.Text.Encoding.ASCII.GetBytes(newId);
        string idBase64 = Convert.ToBase64String(bytesBase64);

        string shortId = new string(idBase64
            .Where(x => char.IsLetterOrDigit(x))
            .ToArray())
            .Substring(0, 10);

        return shortId;
    }
}