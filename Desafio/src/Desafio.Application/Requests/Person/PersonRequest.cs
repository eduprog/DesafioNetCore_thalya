using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class PersonRequest
{

    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; } = Guid.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    public string Name { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    public string City { get; set; } = string.Empty;

    public bool Enable { get; set; } = true;

    public bool CanBuy { get; set; } = false;

    public string Notes { get; set; } = string.Empty;

    public string AlternativeCode { get; set; } = string.Empty;
}
