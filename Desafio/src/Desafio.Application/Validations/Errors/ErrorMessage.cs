namespace Desafio.Application;

public class ErrorMessage
{
    public ErrorMessage(string errorMessage)
    {
        Error = errorMessage;
    }

    public string Error { get; }
}
