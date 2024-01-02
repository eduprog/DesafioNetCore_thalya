using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UnitRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Acronym { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Description { get; set; }
}
