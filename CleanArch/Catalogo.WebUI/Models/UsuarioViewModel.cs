using System.ComponentModel.DataAnnotations;

namespace Catalogo.WebUI.Models;

public class UsuarioViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}