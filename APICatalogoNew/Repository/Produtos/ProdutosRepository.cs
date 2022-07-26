using APICatalogoNew.Context;
using APICatalogoNew.Models;

namespace APICatalogoNew.Repository.Produtos;

public class ProdutosRepository : Repository<Produto>, IProdutosRepository
{
    public ProdutosRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorPreco()
    {
        return Get().OrderBy(p => p.Preco).ToList();
    }
}