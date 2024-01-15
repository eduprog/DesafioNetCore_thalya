using Desafio.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class InsertPersonRequest
{
    private string _document;

    [Required(ErrorMessage = "The field {0} is required")]
    public string Name { get; set; } = string.Empty;

    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }

    [Required(ErrorMessage = "The field {0} is required")]
    public string City { get; set; } = string.Empty;

    public bool Enable { get; set; } = true;

    [DefaultValue(false)]
    public bool CanBuy { get; set; }

    public string Notes { get; set; } = string.Empty;

    public string AlternativeCode { get; set; } = string.Empty;
}
