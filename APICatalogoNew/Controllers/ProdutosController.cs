using APICatalogoNew.DTOs;
using APICatalogoNew.Models;
using APICatalogoNew.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogoNew.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        _mapper = mapper;
    }

    // GET: api/produtos
    [HttpGet("menorpreco")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPreco()
    {
        var produtos = _uof.ProdutosRepository.GetProdutosPorPreco();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    // GET: api/produtos
    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutosRepository.Get().ToList();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    // GET api/produtos/5
    [HttpGet("{id:int}", Name = "GetProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutosRepository.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound();
        }

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return produtoDto;
    }

    // POST api/produtos
    [HttpPost]
    public ActionResult Post(ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _uof.ProdutosRepository.Add(produto);
        _uof.Commit();

        return CreatedAtRoute("GetProduto", new { id = produtoDto.ProdutoId }, produtoDto);
    }

    // PUT api/produtos/5
    [HttpPut("{id:int}")]
    public IActionResult Put(int id, ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDTO);

        _uof.ProdutosRepository.Update(produto);
        _uof.Commit();

        return Ok();
    }

    // DELETE api/produtos/5
    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutosRepository.GetById(p => p.ProdutoId == id);
        if (produto == null)
        {
            return NotFound();
        }

        _uof.ProdutosRepository.Delete(produto);
        _uof.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return produtoDTO;
    }
}