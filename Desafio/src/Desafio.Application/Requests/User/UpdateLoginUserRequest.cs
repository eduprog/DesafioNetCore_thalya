using Desafio.Domain;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class UpdateLoginUserRequest
{
    [Required(ErrorMessage = "The field {0} is required.")]
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracteres.", MinimumLength = 6)]
    public string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "The field {0} and Password must be the same.")]
    public string ConfirmNewPassword { get; set; }

}
