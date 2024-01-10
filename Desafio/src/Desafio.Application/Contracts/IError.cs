namespace Desafio.Application;

public interface IError
{
    bool HasError();
    List<ErrorMessage> GetErrors();
    void Handle(ErrorMessage errorMessage);
}
