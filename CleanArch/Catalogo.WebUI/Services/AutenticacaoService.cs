using Catalogo.WebUI.Models;
using System.Text;
using System.Text.Json;

namespace Catalogo.WebUI.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private const string apiEndpoint = "/api/autoriza/login";

    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private TokenViewModel tokenUsuario;

    public AutenticacaoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioVM)
    {
        var client = _clientFactory.CreateClient("Autenticapi");

        var usuario = JsonSerializer.Serialize(usuarioVM);
        StringContent content = new(usuario, Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            tokenUsuario = await JsonSerializer
                .DeserializeAsync<TokenViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        };

        return tokenUsuario;
    }
}
