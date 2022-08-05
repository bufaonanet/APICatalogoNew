namespace Catalogo.Domain.Account;

public interface IAuthenticate
{
    public Task<bool> Authenticate(string email, string password);
    public Task<bool> RegisterUser(string email, string password);
    public Task Logout();
}