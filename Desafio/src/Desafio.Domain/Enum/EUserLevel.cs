using System.ComponentModel;

namespace Desafio.Domain.Enum;

public enum EUserLevel
{
    [Description("Seller")]
    Seller = 1,
    [Description("Manager")]
    Manager = 2,
    [Description("Administrator")]
    Administrator = 3
}
