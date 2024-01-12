using Desafio.Domain;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateUserRequest
{
    private string _document;

    [Required(ErrorMessage = "The field {0} is required.")]
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string Email { get; set; }

    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string UserName { get; set; }

    [Range(1, 3, ErrorMessage = "The field {0} must be between 1 and 3. Values: 1-Seller, 2-Mananger or 3-Administrator.")]
    public EUserLevel UserLevel { get; set; } = EUserLevel.Administrator;

    public string Name { get; set; }

    public string NickName { get; set; }

    public string Document
    {
        get => _document;
        set => _document = value.GetOnlyDocumentNumber();
    }
}
