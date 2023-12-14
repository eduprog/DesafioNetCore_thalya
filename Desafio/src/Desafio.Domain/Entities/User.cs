using Desafio.Domain.Enum;

namespace Desafio.Domain;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public EUserLevel UserLevel { get; set; } = EUserLevel.Seller;
}
