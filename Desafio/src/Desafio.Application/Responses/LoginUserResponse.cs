using System.Text.Json.Serialization;

namespace Desafio.Application;

public class LoginUserResponse
{
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; set; }
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime DataExpiration { get; set; }
    public List<string> Errors { get; set; }

    //Inicializa Lista de Erros
    public LoginUserResponse() => Errors = new List<string>();
    //Construtor
    public LoginUserResponse(bool success = true) : this()
    {
        Success = success;
    }
    public LoginUserResponse(bool success, string token, DateTime dataExpiration) : this(success)
    {
        Token = token;
        DataExpiration = dataExpiration;
    }

    //Adicionar lista de erros se tiver
    public void InsertErrors(IEnumerable<string> errors) 
    { 
        Errors.AddRange(errors); 
    }
    public void InsertError(string error) 
    {
        Errors.Add(error); 
    }
}
