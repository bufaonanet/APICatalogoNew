using APICatalogoNew.Repository.Categorias;
using APICatalogoNew.Repository.Produtos;

namespace APICatalogoNew.Repository;

public interface IUnitOfWork
{
    IProdutosRepository ProdutosRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task<bool> CommitAsync();
}