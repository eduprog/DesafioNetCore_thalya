using Microsoft.IdentityModel.Tokens;

namespace Desafio.Identity;

public class JwtOptions
{
    public string Secret { get; set; }
    public int ExpirationHour { get; set; }
    public string Sender { get; set; }
    public string ValidIn { get; set; }
}
