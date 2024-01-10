using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class EnabledProductRequest
{
    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public bool Enable { get; set; } = true;
}
