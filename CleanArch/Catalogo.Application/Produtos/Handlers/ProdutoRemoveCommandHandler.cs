using Catalogo.Application.Produtos.Commands;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MediatR;

namespace Catalogo.Application.Produtos.Handlers;

public class ProdutoRemoveCommandHandler : IRequestHandler<ProdutoRemoveCommand, Produto>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoRemoveCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Produto> Handle(ProdutoRemoveCommand request, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.GetByIdAsync(request.Id);
        if (produto is null)
        {
            throw new ApplicationException($"Error could not be found.");
        }

        var result =  await _produtoRepository.RemoveAsync(produto);
        return result;
    }
}
