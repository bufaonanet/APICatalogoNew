using System.ComponentModel.DataAnnotations;

namespace APICatalogoNew.DTOs;

public class CategoriaDTO
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório")]
    [StringLength(80, ErrorMessage = "O {0} deve ter o máximo de {1} caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "O {0} deve ter o máximo de {1} caracteres")]
    public string? ImagemUrl { get; set; }
    public ICollection<ProdutoDTO>? Produtos { get; set; }
}
