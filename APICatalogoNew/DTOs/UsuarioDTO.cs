using System.ComponentModel.DataAnnotations;

namespace APICatalogoNew.DTOs;

public class CreateUsuarioDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage ="Confirmação de senha não confere.")]
    public string? ConfirmPassword { get; set; }
}