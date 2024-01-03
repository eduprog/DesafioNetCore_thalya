using Desafio.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Application;

public class RegisterUserRequest
{
    [Required(ErrorMessage = "The field {0} is required.")]
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracteres.", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The field {0} and Password must be the same.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [Range(1, 3, ErrorMessage = "The field {0} must be between 1 and 3. Values: 1-Seller, 2-Mananger or 3-Administrator.")]
    public EUserLevel Role { get; set; } = EUserLevel.Administrator;

}
