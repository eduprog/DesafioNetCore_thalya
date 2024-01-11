namespace Desafio.Domain;

public class Product : Entity
{
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal StoredQuantity { get; set; }
    public bool Enable { get; set; } = true;
    public bool Sellable { get; set; } 
    public string BarCode { get; set; } = string.Empty;

    #region Navigation Properties
    public Unit Unit { get; set; }
    #endregion

}
