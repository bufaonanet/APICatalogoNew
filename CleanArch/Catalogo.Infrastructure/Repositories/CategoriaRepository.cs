using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private ApplicationDbContext _categoryContext;

    public CategoriaRepository(ApplicationDbContext categoryContext)
    {
        _categoryContext = categoryContext ?? 
            throw new ArgumentNullException(nameof(categoryContext));
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
    {
        return await _categoryContext.Categorias.ToListAsync();
    }

    public async Task<Categoria> GetByIdAsync(int? id)
    {
        return await _categoryContext.Categorias.FindAsync(id);
    }

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        _categoryContext.Add(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        _categoryContext.Update(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> RemoveAsync(Categoria categoria)
    {
        _categoryContext.Remove(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }    
}