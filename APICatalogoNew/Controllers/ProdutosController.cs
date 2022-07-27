using APICatalogoNew.DTOs;
using APICatalogoNew.Models;
using APICatalogoNew.Pagination;
using APICatalogoNew.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APICatalogoNew.Controllers;

[Authorize]
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
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPreco()
    {
        var produtos = await _uof.ProdutosRepository.GetProdutosPorPreco();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    // GET: api/produtos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters parameters)
    {
        var produtos = await _uof.ProdutosRepository.GetProdutos(parameters);

        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }

    // GET api/produtos/5
    [HttpGet("{id:int}", Name = "GetProduto")]
    [AllowAnonymous]
    public async Task<ActionResult<ProdutoDTO>> Get(int id)
    {
        var produto = await _uof.ProdutosRepository.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound();
        }

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return produtoDto;
    }

    // POST api/produtos
    [HttpPost]
    public async Task<ActionResult> Post(ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _uof.ProdutosRepository.Add(produto);
        await _uof.CommitAsync();

        _mapper.Map(produto, produtoDto);

        return CreatedAtRoute("GetProduto", new { id = produtoDto.ProdutoId }, produtoDto);
    }

    // PUT api/produtos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDTO);

        _uof.ProdutosRepository.Update(produto);
        await _uof.CommitAsync();

        return Ok();
    }

    // DELETE api/produtos/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _uof.ProdutosRepository.GetById(p => p.ProdutoId == id);
        if (produto == null)
        {
            return NotFound();
        }

        _uof.ProdutosRepository.Delete(produto);
        await _uof.CommitAsync();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return produtoDTO;
    }
}