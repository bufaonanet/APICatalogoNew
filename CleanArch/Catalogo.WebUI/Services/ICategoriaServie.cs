using Catalogo.WebUI.Models;

namespace Catalogo.WebUI.Services;

public interface ICategoriaServie
{
    Task<IEnumerable<CategoriaViewModel>> GetCategorias();
    Task<CategoriaViewModel> GetCategoriaPorId(int id);
    Task<CategoriaViewModel> CriaCategoria(CategoriaViewModel categoriaVM);
    Task<bool> AtualizaCategoria(int id, CategoriaViewModel categoriaVM);
    Task<bool> DeletaCategoria(int id);
}