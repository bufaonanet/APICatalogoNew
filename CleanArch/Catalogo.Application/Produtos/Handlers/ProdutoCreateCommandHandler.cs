using Catalogo.Application.Produtos.Commands;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MediatR;

namespace Catalogo.Application.Produtos.Handlers;

public class ProdutoCreateCommandHandler : IRequestHandler<ProdutoCreateCommand, Produto>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoCreateCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> Handle(ProdutoCreateCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto(request.Nome, request.Descricao, request.Preco,
            request.ImagemUrl, request.Estoque, request.DataCadastro);

        if (produto is null)
        {
            throw new ApplicationException($"Error creating entity {nameof(Produto)}");
        }

        produto.CategoriaId = request.CategoriaId;
        return await _produtoRepository.CreateAsync(produto);
    }
}