using Catalogo.WebUI.Models;
using Catalogo.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Catalogo.WebUI.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaServie _categoriaServie;
    private string token = string.Empty;

    public ProdutosController(IProdutoService produtoService,
                              ICategoriaServie categoriaServie)
    {
        _produtoService = produtoService;
        _categoriaServie = categoriaServie;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.GetProdutos(ObtemTokenJwt());
        if (produtos is null)
            return View("Error");

        return View(produtos);
    }


    [HttpGet]
    public async Task<IActionResult> CriarNovoProduto()
    {
        ViewBag.CategoriaId = new SelectList(
            await _categoriaServie.GetCategorias(), "Id", "Nome");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarNovoProduto(ProdutoViewModel requestProduto)
    {
        if (ModelState.IsValid)
        {
            var produto = await _produtoService.CriaProduto(requestProduto, ObtemTokenJwt());
            if (produto != null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoriaId = new SelectList(
            await _categoriaServie.GetCategorias(), "Id", "Nome");

        return View(requestProduto);
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarProduto(int id)
    {
        var produto = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());
        if (produto is null)
            return View("Error");

        ViewBag.CategoriaId = new SelectList(
            await _categoriaServie.GetCategorias(), "Id", "Nome");

        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarProduto(int id, ProdutoViewModel requestProduto)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.AtualizaProduto(id, requestProduto, ObtemTokenJwt());
            if (result)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoriaId = new SelectList(
            await _categoriaServie.GetCategorias(), "Id", "Nome");

        return View(requestProduto);
    }

    [HttpGet]
    public async Task<IActionResult> DeletarProduto(int id)
    {
        var produto = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());
        if (produto is null)
            return View("Error");

        return View(produto);
    }

    [HttpPost, ActionName("DeletarProduto")]
    public async Task<IActionResult> DeletaConfirmado(int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.DeletaProduto(id, ObtemTokenJwt());
            if (result)
                return RedirectToAction(nameof(Index));
        }

        return View("Error");
    }


    private string ObtemTokenJwt()
    {
        if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
            token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

        return token;
    }
}
