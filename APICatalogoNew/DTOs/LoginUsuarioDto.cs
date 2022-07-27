using System.ComponentModel.DataAnnotations;

namespace APICatalogoNew.DTOs;

public class LoginUsuarioDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
