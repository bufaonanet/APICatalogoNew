using Catalogo.Application.Produtos.Queries;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MediatR;

namespace Catalogo.Application.Produtos.Handlers;

public class GetProdutoByIdQueryHandler : IRequestHandler<GetProdutoByIdQuery, Produto>
{
    private readonly IProdutoRepository _produtoRepository;

    public GetProdutoByIdQueryHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public Task<Produto> Handle(GetProdutoByIdQuery request, CancellationToken cancellationToken)
    {
        return _produtoRepository.GetByIdAsync(request.Id); 
    }
}

