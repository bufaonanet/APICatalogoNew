using Catalogo.Domain.Entities;
using MediatR;

namespace Catalogo.Application.Produtos.Queries;

public class GetProdutoByIdQuery : IRequest<Produto>
{
    public int Id { get; set; }

    public GetProdutoByIdQuery(int id)
    {
        Id = id;
    }
}