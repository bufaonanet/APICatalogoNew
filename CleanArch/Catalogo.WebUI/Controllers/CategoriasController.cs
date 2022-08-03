using Catalogo.WebUI.Models;
using Catalogo.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.WebUI.Controllers;

public class CategoriasController : Controller
{
    private readonly ICategoriaServie _categoriaServie;

    public CategoriasController(ICategoriaServie categoriaServie)
    {
        _categoriaServie = categoriaServie ??
            throw new ArgumentNullException(nameof(categoriaServie));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _categoriaServie.GetCategorias();
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public IActionResult CriarNovaCategoria()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarNovaCategoria(CategoriaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaServie.CriaCategoria(model);
            if (result != null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Erro ao criar Categoria";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarCategoria(int id)
    {
        var result = await _categoriaServie.GetCategoriaPorId(id);
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarCategoria(int id, CategoriaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaServie.AtualizaCategoria(id, model);
            if (result)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Erro ao atualizar Categoria";
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DeletarCategoria(int id)
    {
        var result = await _categoriaServie.GetCategoriaPorId(id);
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost, ActionName("DeletarCategoria")]
    public async Task<IActionResult> DeletaConfirmado(int id)
    {
        var result = await _categoriaServie.DeletaCategoria(id);
        if (result)
            return RedirectToAction(nameof(Index));

        return View("Error");
    }



}
