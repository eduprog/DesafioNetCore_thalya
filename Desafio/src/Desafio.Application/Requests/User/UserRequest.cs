using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UserRequest
{
    [Required(ErrorMessage = "The field {0} is required.")]
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string UserName { get; set; } = string.Empty;
}
