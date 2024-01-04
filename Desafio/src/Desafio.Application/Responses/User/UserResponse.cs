using Desafio.Domain;
using System.Text.Json.Serialization;

namespace Desafio.Application;

public class UserResponse
{
    public bool Success { get; set; }
    public User User { get; set; }
    public List<User> Users { get; set; }
    public List<string> Errors { get; set; }

    //Inicializa Lista de Erros
    public UserResponse()
    {
        Errors = new List<string>();
        Users = new List<User>();
    }
    //Construtor
    public UserResponse(bool success = true, User user = null) : this()
    {
        Success = success;
        User = user;
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
    //Adicionar unidades selecionadas na lista de unidades da classe response
    public void InsertUnits(List<User> users)
    {
        Users.AddRange(users);
    }
}
