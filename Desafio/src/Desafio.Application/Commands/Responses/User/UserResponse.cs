using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Desafio.Application;

public class UserResponse
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
    public string ShortId { get; set; }
}
