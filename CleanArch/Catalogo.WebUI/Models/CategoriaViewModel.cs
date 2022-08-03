using System.ComponentModel.DataAnnotations;

namespace Catalogo.WebUI.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome da imagem")]
        [MinLength(5)]
        [MaxLength(250)]
        [Display(Name ="Imagem")]
        public string? ImagemUrl { get; set; }
    }
}
