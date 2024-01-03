using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UnitRequest
{
    [Required(ErrorMessage = "The field {0} is required")]
    public string Acronym { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public string Description { get; set; }
    //public Guid Id { get; set; }
}
