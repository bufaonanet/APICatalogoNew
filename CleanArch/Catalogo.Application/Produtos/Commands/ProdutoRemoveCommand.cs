using Catalogo.Domain.Entities;
using MediatR;

namespace Catalogo.Application.Produtos.Commands;

public class ProdutoRemoveCommand : IRequest<Produto>
{
    public int Id { get; set; }

    public ProdutoRemoveCommand(int id)
    {
        Id = id;
    }
}