using Catalogo.Domain.Entities;
using MediatR;

namespace Catalogo.Application.Produtos.Queries;

public class GetProdutosQuery : IRequest<IEnumerable<Produto>>
{
   
}

