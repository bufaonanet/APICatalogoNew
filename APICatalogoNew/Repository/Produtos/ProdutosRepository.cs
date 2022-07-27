using APICatalogoNew.Context;
using APICatalogoNew.Models;
using APICatalogoNew.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoNew.Repository.Produtos;

public class ProdutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters parameters)
    {
        return await PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.ProdutoId),
            parameters.PageNamber, parameters.PageSize);
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
    {
        return await Get().OrderBy(p => p.Preco).ToListAsync();
    }
}