using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class PersonResponse
{
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public bool Enable { get; set; } = true;

    public bool CanBuy { get; set; } = false;

    public string Notes { get; set; } = string.Empty;

    public string AlternativeCode { get; set; } = string.Empty;

    public string ShortId { get; set; } = string.Empty;
}
