﻿using Microsoft.AspNetCore.Mvc;
using APICatalogoNew.Models;
using APICatalogoNew.Filters;
using APICatalogoNew.Repository;
using AutoMapper;
using APICatalogoNew.DTOs;
using APICatalogoNew.Pagination;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace APICatalogoNew.Controllers;

[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;
    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork uof,
                                ILogger<CategoriasController> logger,
                                IMapper mapper)
    {
        _uof = uof;
        _logger = logger;
        _mapper = mapper;
    }

    #region testes
    //[HttpGet("/saudacao/{nome}")]
    //public ActionResult<string> Teste(string nome, [FromServices] IMeuServico meuServico)
    //{
    //    return meuServico.Saudacao(nome);
    //}
    #endregion

    [HttpGet]
    //[ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias([FromQuery] CategoriasParameters parameters )
    {
        try
        {
            _logger.LogInformation("==================== GET api/categorias ===================");

            var categorias = await _uof.CategoriaRepository.GetCategoriasPaginado(parameters);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        try
        {
            _logger.LogInformation("================ GET api/categorias/produtos ==============");

            var categorias = await _uof.CategoriaRepository.GetCategoriasProdutos();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o obter Categoria");
        }
    }

    /// <summary>
    /// Obtem uma Categoria pleo seu Id
    /// </summary>
    /// <param name="id">Código da Categoria</param>
    /// <returns>Objeto Categoria</returns>
    [HttpGet("{id}", Name = "GetCategoria")]
    [ProducesResponseType(typeof(CategoriaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
    {
        try
        {
            _logger.LogInformation($"================ GET api/categorias/{id} ==============");

            var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogInformation($"================ GET api/categorias/{id} NOT FOUND=============");
                return NotFound($"Nenhuma Categoria para o Id={id}");
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o banco");
        }
    }

    /// <summary>
    /// Inclui uma nova categoria
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST api/categorias
    ///     {
    ///        "categoriaId": 1,
    ///        "nome": "categoria1",
    ///        "imagemUrl": "http://teste.net/1.jpg"
    ///     }
    /// </remarks>
    /// <param name="categoriaDTO">objeto Categoria</param>
    /// <returns>O objeto Categoria incluida</returns>
    /// <remarks>Retorna um objeto Categoria incluído</remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDTO>> PostCategoria(CategoriaDTO categoriaDTO)
    {
        try
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uof.CategoriaRepository.Add(categoria);
            await _uof.CommitAsync();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("GetCategoria", new { id = categoriaDto.CategoriaId }, categoriaDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao criar uma nova Categoria");
        }
    }

    /// <summary>
    /// Altera uma Categoria
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoriaDTO"></param>
    /// <returns>retorna 400 ou 200</returns>
    [HttpPut("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutCategoria(int id, [FromBody] CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
            {
                return BadRequest($"Não foi possível atualizar categoria com id={id}");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uof.CategoriaRepository.Update(categoria);
            await _uof.CommitAsync();

            return Ok($"Categoria com id={id} atualizada com sucesso!");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro tentar atualizar Categoria com id={id}");
        }
    }

    /// <summary>
    /// Deleta uma Categoria
    /// </summary>
    /// <param name="id">codigo da categoria (int) </param>
    /// <returns>CategoriaDTO</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> DeleteCategoria(int id)
    {
        try
        {
            var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound($"Nenhuma Categoria para o Id={id}");
            }

            _uof.CategoriaRepository.Delete(categoria);
            await _uof.CommitAsync();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro tentar excluir Categoria com id={id}");
        }
    }
}