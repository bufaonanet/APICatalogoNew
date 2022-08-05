using Catalogo.Application.Produtos.Commands;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MediatR;

namespace Catalogo.Application.Produtos.Handlers;

public class ProdutoUpdateCommandHandler : IRequestHandler<ProdutoUpdateCommand, Produto>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoUpdateCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> Handle(ProdutoUpdateCommand request, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.GetByIdAsync(request.Id);
        if (produto is null)
        {
            throw new ApplicationException($"Error could not be found.");
        }

        produto.Update(request.Nome, request.Descricao, request.Preco,
            request.ImagemUrl, request.Estoque, request.DataCadastro, request.CategoriaId);

        return await _produtoRepository.UpdateAsync(produto);
    }
}