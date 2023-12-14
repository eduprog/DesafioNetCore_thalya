namespace Desafio.Domain;

public class Person : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public bool CanSell { get; set; } = false;
    public string Notes { get; set; } = string.Empty;
    public string AlternativeCode { get; set; } = string.Empty;
}
