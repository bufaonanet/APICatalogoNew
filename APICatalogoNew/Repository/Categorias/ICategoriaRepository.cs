using APICatalogoNew.Models;

namespace APICatalogoNew.Repository.Categorias;

public interface ICategoriaRepository: IRepository<Categoria>
{
    IEnumerable<Categoria> GetCategoriasProdutos();
}
