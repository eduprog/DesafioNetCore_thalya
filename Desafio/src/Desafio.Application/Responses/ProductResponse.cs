namespace Desafio.Application;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Acronym { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortId { get; set; }
}
