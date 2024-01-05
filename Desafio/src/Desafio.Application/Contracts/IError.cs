using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio.Application;

public interface IError
{
    bool HasError();
    List<ErrorMessage> GetErrors();
    void Handle(ErrorMessage errorMessage);
}
