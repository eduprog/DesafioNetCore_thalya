using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UnitRequest
{
    private string _acronym;
    private string _description;

    [Required(ErrorMessage = "The field {0} is required")]
    public string Acronym 
    {
        get => _acronym;
        set => _acronym = value?.ToUpper();
    }
[Required(ErrorMessage = "The field {0} is required")]
    public string Description
    {
        get => _description;
        set => _description = value?.ToUpper();
    }
}
