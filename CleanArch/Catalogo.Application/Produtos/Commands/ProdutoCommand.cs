using Catalogo.Domain.Entities;
using MediatR;

namespace Catalogo.Application.Produtos.Commands;

public abstract class ProdutoCommand : IRequest<Produto>
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
}