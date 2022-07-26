using APICatalogoNew.Context;
using APICatalogoNew.Extensions;
using APICatalogoNew.Filters;
using APICatalogoNew.Repository;
using APICatalogoNew.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace APICatalogoNew;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string mySqlConnection = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IMeuServico, MeuServico>();
        services.AddScoped<ApiLoggingFilter>();

        services.AddControllers()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {          
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseConfigExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
