namespace Desafio.Domain;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ShortId { get; set; } = GenerateShortId.GetShortId();
}
