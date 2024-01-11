namespace Desafio.Application;

public class ProductResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal StoredQuantity { get; set; }
    public bool Enable { get; set; } = true;
    public bool Sellable { get; set; } 
    public string BarCode { get; set; } = string.Empty;
    public string ShortId { get; set; } = string.Empty;
}
