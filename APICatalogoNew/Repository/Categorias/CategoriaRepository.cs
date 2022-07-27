using APICatalogoNew.Context;
using APICatalogoNew.Models;
using APICatalogoNew.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoNew.Repository.Categorias;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Categoria>> GetCategoriasPaginado(CategoriasParameters parameters)
    {
        return await PagedList<Categoria>.ToPagedList(Get().OrderBy(c => c.Nome),
           parameters.PageNamber, parameters.PageSize);
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await Get().Include(c => c.Produtos).ToListAsync();
    }
}
