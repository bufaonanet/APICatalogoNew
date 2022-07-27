using APICatalogoNew.Context;
using APICatalogoNew.Repository.Categorias;
using APICatalogoNew.Repository.Produtos;

namespace APICatalogoNew.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ProdutosRepository? _produtosRepo;
    private CategoriaRepository? _categoriaRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }


    public IProdutosRepository ProdutosRepository
    {
        get { return _produtosRepo ?? new ProdutosRepository(_context); }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get { return _categoriaRepo ?? new CategoriaRepository(_context); }

    }
    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}