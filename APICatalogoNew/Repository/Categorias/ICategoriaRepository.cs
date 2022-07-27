using APICatalogoNew.Models;
using APICatalogoNew.Pagination;

namespace APICatalogoNew.Repository.Categorias;

public interface ICategoriaRepository: IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasPaginado(CategoriasParameters parameters);
    Task<IEnumerable<Categoria>> GetCategoriasProdutos();
}
