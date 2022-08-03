using Catalogo.WebUI.Models;
using System.Text;
using System.Text.Json;

namespace Catalogo.WebUI.Services;

public class CategoriaServie : ICategoriaServie
{
    private const string apiEndpoint = "/api/categorias/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CategoriaViewModel? categoriaVM;
    private IEnumerable<CategoriaViewModel>? categoriasVM;

    public CategoriaServie(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoriaViewModel>> GetCategorias()
    {
        var client = _clientFactory.CreateClient("CategoriasApi");
        using var response = await client.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categoriasVM = await JsonSerializer
                .DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        };
        return categoriasVM;
    }

    public async Task<CategoriaViewModel> GetCategoriaPorId(int id)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");
        using var response = await client.GetAsync(apiEndpoint + id);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categoriaVM = await JsonSerializer
                .DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        };
        return categoriaVM;
    }

    public async Task<CategoriaViewModel> CriaCategoria(CategoriaViewModel categoriaVM)
    {
        var client = _clientFactory.CreateClient("Autenticapi");
        var categoria = JsonSerializer.Serialize(categoriaVM);
        StringContent content = new(categoria, Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(apiEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categoriaVM = await JsonSerializer
                .DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        };
        return categoriaVM;
    }

    public async Task<bool> AtualizaCategoria(int id, CategoriaViewModel categoriaVM)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");
        using var response = await client.PutAsJsonAsync(apiEndpoint + id, categoriaVM);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }

    public async Task<bool> DeletaCategoria(int id)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");
        using var response = await client.DeleteAsync(apiEndpoint + id);
        if (response.IsSuccessStatusCode)
            return true;
        return false;
    }
}
  
