using APICatalogoMinimal.Models;

namespace APICatalogoMinimal.Services;

public interface ITokenService
{
    string GerarToken(UserModel user);
}