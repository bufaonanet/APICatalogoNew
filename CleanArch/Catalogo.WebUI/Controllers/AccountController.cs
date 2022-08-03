using Catalogo.WebUI.Models;
using Catalogo.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AccountController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido...");
            return View();
        }

        var result = await _autenticacaoService.AutenticaUsuario(model);

        if (result is null)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido...");
            return View(model);
        }

        //Armazena o token no cookie
        Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Redirect("/");
    }
}
