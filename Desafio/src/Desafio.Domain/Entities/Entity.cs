using System.Buffers.Text;

namespace Desafio.Domain;

public class Entity
{
    public Guid Id { get; set; }
    public string ShortId { get; set; } = string.Empty;
}
