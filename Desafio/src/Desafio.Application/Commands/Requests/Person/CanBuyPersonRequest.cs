using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class CanBuyPersonRequest
{
    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    [DefaultValue(false)]
    public bool CanBuy { get; set; }
    
}
