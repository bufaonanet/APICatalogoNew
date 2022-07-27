
using APICatalogoNew.Models;
using APICatalogoNew.Pagination;

namespace APICatalogoNew.Repository.Produtos;

public interface IProdutosRepository : IRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutos(ProdutosParameters parameters);
    Task<IEnumerable<Produto>> GetProdutosPorPreco();
}