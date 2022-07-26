using APICatalogoNew.Context;
using APICatalogoNew.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoNew.Repository.Categorias;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include(c => c.Produtos).ToList();
    }
}
