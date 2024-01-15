using System.Text.Json.Serialization;

namespace Desafio.Application;

public class LoginUserResponse
{
    public string Email { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; set; }
    public double Expiration { get; set; }
    public string ShortId { get; set; }
}
