using APICatalogoMinimal.Context;
using APICatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoMinimal.ApiEndpoints;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {
        app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync())
           .RequireAuthorization()
           .WithTags("Categorias");

        app.MapGet("/categorias/{id:int}", async (AppDbContext db, int id) =>
        {
            return await db.Categorias.FindAsync(id) is Categoria categoria
                ? Results.Ok(categoria)
                : Results.NotFound();
        });

        app.MapPost("/categorias", async (AppDbContext db, Categoria categoria) =>
        {
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();
            return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
        });

        app.MapPut("/categorias/{id:int}", async (AppDbContext db, int id, Categoria categoriaRequest) =>
        {
            if (categoriaRequest.CategoriaId != id)
                return Results.BadRequest();

            var categoriaDb = await db.Categorias.FindAsync(id);
            if (categoriaDb is null)
                return Results.NotFound();

            categoriaDb.Nome = categoriaRequest.Nome;
            categoriaDb.Descricao = categoriaRequest.Descricao;
            await db.SaveChangesAsync();

            return Results.Ok(categoriaDb);
        });

        app.MapDelete("/categorias/{id:int}", async (AppDbContext db, int id) =>
        {
            var categoria = await db.Categorias.FindAsync(id);
            if (categoria is null)
                return Results.NotFound();

            db.Remove(categoria);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        //app.MapPost("/categorias2", async ([FromServices] AppDbContext db, [FromBody]Categoria categoria) =>
        //{
        //    db.Categorias?.Add(categoria);
        //    await db.SaveChangesAsync();
        //    return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
        //}).Accepts<Categoria>("application/json")
        //  .Produces<Categoria>(StatusCodes.Status201Created)
        //  .WithName("CriarNovaCategoria")
        //  .WithTags("Setter");

        // Configure the HTTP request pipeline. Configure
    }
}
