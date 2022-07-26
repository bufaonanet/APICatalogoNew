
using APICatalogoNew.Models;

namespace APICatalogoNew.Repository.Produtos;

public interface IProdutosRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorPreco();
}