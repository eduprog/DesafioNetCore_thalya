namespace Desafio.Application;

public class RegisterUserResponse
{
    public bool Success { get; set; }
    public List<string> Errors { get; set;}
    
    //Inicializa Lista de Erros
    public RegisterUserResponse() => Errors = new List<string>();
    //Construtor
    public RegisterUserResponse(bool success = true) : this()
    {
        Success = success;
    }

    //Adicionar lista de erros se tiver
    public void InsertErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
}
