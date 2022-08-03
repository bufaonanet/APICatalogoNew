using Catalogo.WebUI.Models;

namespace Catalogo.WebUI.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
    Task<ProdutoViewModel> GetProdutoPorId(int id, string token);
    Task<ProdutoViewModel> CriaProduto(ProdutoViewModel produtoRequest, string token);
    Task<bool> AtualizaProduto(int id, ProdutoViewModel produtoRequest, string token);
    Task<bool> DeletaProduto(int id, string token);
}