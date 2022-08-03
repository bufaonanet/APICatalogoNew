using Catalogo.WebUI.Models;

namespace Catalogo.WebUI.Services;

public interface IAutenticacaoService
{
    Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel model);
}