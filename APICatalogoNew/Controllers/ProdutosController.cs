using APICatalogoNew.Context;
using APICatalogoNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoNew.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // GET: api/<ProdutosController>
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos?.AsNoTracking().ToList();
        if (produtos is null)
        {
            return NoContent();
        }
        return produtos;
    }

    // GET api/<ProdutosController>/5
    [HttpGet("{id:int}", Name = "GetProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos?
            .AsNoTracking()
            .FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Produto com Id {id} não encontrado..");
        }
        return produto;
    }

    // POST api/<ProdutosController>
    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos?.Add(produto);
        _context.SaveChanges();

        return CreatedAtRoute("GetProduto", new { id = produto.ProdutoId }, produto);

    }

    // PUT api/<ProdutosController>/5
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    // DELETE api/<ProdutosController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos?.Find(id);
        if (produto is null)
        {
            return BadRequest($"Produto com Id {id} não encontrado..");
        }

        _context.Produtos?.Remove(produto);
        _context.SaveChanges();

        return Ok();
    }
}