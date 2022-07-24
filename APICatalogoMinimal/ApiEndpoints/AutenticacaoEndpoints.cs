using APICatalogoMinimal.Models;
using APICatalogoMinimal.Services;
using Microsoft.AspNetCore.Authorization;

namespace APICatalogoMinimal.ApiEndpoints;

public static class AutenticacaoEndpoints
{
    public static void MapAutenticacaoEndpoints(this WebApplication app)
    {
        app.MapPost("/login", [AllowAnonymous] (UserModel usermode, ITokenService tokenService) =>
        {
            if (usermode is null)
                return Results.BadRequest("Login Inválido");

            if (usermode.UserName == "admin" && usermode.Password == "123456")
            {
                var token = tokenService.GerarToken(usermode);
                return Results.Ok(new { token });
            }
            else
            {
                return Results.BadRequest("Login Inválido");
            }
        }).Produces(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status200OK)
          .WithName("Login")
          .WithTags("Autenticacao");
    }
}