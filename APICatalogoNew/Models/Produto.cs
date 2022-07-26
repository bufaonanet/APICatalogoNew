using APICatalogoNew.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogoNew.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, ErrorMessage = "O {0} deve ter entre {1} e {2} caracteres", MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }


    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "A {0} deve ter o máximo de {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 1000, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(Nome))
        {
            var primemiraLetra = Nome[0].ToString();
            if (primemiraLetra != primemiraLetra.ToUpperInvariant())
            {
                yield return new ValidationResult("A primeira letra do produto deve ser maiúscula", new[] { nameof(Nome) });
                //yield break;
            }
        };

        if (Estoque <= 0)
        {
            yield return new ValidationResult("O estoque deve ser maior que zero", new[] { nameof(Estoque) });
            //yield break;
        };
    }
}