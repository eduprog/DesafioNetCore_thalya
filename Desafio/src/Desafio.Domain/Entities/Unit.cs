namespace Desafio.Domain;

public class Unit : Entity
{
    public string Acronym { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set;} = new List<Product>();
}
