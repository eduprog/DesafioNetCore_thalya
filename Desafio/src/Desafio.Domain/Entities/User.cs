using Microsoft.AspNetCore.Identity;

namespace Desafio.Domain;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
}
