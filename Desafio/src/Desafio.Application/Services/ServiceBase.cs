using Desafio.Domain;
using FluentValidation;
using FluentValidation.Results;

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
}