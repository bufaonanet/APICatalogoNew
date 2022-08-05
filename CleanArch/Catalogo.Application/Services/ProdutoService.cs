using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Application.Produtos.Commands;
using Catalogo.Application.Produtos.Queries;
using MediatR;

namespace Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{   
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProdutoService(IMapper mapper,
                          IMediator mediator)
    {        
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var productsQuery = new GetProdutosQuery();
        if (productsQuery is null)
            throw new ApplicationException($"Entity coud not be loaded. {nameof(GetProdutosQuery)}");

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProdutoDTO>>(result);
    }

    public async Task<ProdutoDTO> GetById(int? id)
    {
        var productsQueryById = new GetProdutoByIdQuery(id.Value);
        if (productsQueryById is null)
            throw new ApplicationException($"Entity coud not be loaded. {nameof(GetProdutoByIdQuery)}");

        var result = await _mediator.Send(productsQueryById);

        return _mapper.Map<ProdutoDTO>(result);
    }

    public async Task Add(ProdutoDTO produtoDto)
    {
        var productCreateCommand = _mapper.Map<ProdutoCreateCommand>(produtoDto);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProdutoDTO produtoDto)
    {
        var productCreateCommand = _mapper.Map<ProdutoUpdateCommand>(produtoDto);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProdutoRemoveCommand(id.Value);
        if (productRemoveCommand is null)
            throw new ApplicationException($"Entity coud not be loaded. {nameof(ProdutoRemoveCommand)}");

        await _mediator.Send(productRemoveCommand);
    }
}
