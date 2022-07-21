using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalogoNew.Context;
using APICatalogoNew.Models;

namespace APICatalogoNew.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        try
        {
            if (_context.Categorias == null)
                return NotFound("Nenhuma Categoria encontrada");

            return await _context.Categorias.AsNoTracking().ToListAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasComProdutos()
    {
        try
        {
            if (_context.Categorias == null)
                return NotFound();

            return await _context.Categorias.Include(c => c.Produtos).ToListAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }

    }

    [HttpGet("{id}", Name = "GetCategoria")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        try
        {
            if (_context.Categorias == null)
                return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com Id={id} não encontrada");
            }
            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
       
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {
        if (_context.Categorias == null)
        {
            return Problem("Entity set 'AppDbContext.Categorias'  is null.");
        }
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtRoute("GetCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        if (_context.Categorias == null)
        {
            return NotFound();
        }
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
