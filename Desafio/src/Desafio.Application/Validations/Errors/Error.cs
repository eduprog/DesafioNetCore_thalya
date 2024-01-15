namespace Desafio.Application;

public class Error : IError
{
    private List<ErrorMessage> _errorMessages;

    public Error()
    {
        _errorMessages = new List<ErrorMessage>();
    }

    public void Handle(ErrorMessage errorMessage)
    {
        _errorMessages.Add(errorMessage);
    }

    public List<ErrorMessage> GetErrors()
    {
        return _errorMessages;
    }

    public bool HasError()
    {
        return _errorMessages.Any();
    }
}
