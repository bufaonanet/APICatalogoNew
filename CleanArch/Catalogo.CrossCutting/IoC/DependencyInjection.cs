using Catalogo.Application.Interfaces;
using Catalogo.Application.Mappings;
using Catalogo.Application.Services;
using Catalogo.Domain.Account;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Identity;
using Catalogo.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureApi(
        this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(options =>
        // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
        //), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        string mySqlConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                             p => p.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        var myHandlers = AppDomain.CurrentDomain.Load("Catalogo.Application");
        services.AddMediatR(myHandlers);

        return services;
    }
}
