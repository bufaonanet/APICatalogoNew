using System.ComponentModel.DataAnnotations;

namespace Catalogo.WebUI.Models;

public class ProdutoViewModel
{
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, ErrorMessage = "O {0} deve ter entre {1} e {2} caracteres", MinimumLength = 5)]

    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "A {0} deve ter o máximo de {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }

    public int CategoriaId { get; set; }
}
