using Catalogo.Application.Produtos.Queries;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MediatR;

namespace Catalogo.Application.Produtos.Handlers;

public class GetProdutosQueryHandler : IRequestHandler<GetProdutosQuery, IEnumerable<Produto>>
{
    private readonly IProdutoRepository _produtoRepository;

    public GetProdutosQueryHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Produto>> Handle(GetProdutosQuery request, CancellationToken cancellationToken)
    {
        return await _produtoRepository.GetProdutosAsync();
    }
}

