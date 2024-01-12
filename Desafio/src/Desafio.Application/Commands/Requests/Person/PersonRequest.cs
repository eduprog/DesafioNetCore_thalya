using Desafio.Domain;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class PersonRequest
{
    private string _document;

    [Required(ErrorMessage = "The field {0} is required")]
    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }

    public string City { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public string AlternativeCode { get; set; } = string.Empty;
}
