using APICatalogoMinimal.ApiEndpoints;
using APICatalogoMinimal.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.//ConfigureServices
builder.AddApiSwagger();
builder.AddPersistence();
builder.AddAutenticatioinJwt();
builder.Services.AddCors();

var app = builder.Build();
// Configure the HTTP request pipeline.//Configure

//Para testar HealthCheck
//app.MapGet("/", () => $"Health check at {DateTime.Now}").ExcludeFromDescription(); 

app.MapAutenticacaoEndpoints();
app.MapCategoriasEndpoints();
app.MapProdutosEndpoints();

var enviroment = app.Environment;
app.UseExceptionHandling(enviroment)
   .UseAppCors()
   .UserSwaggerMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.Run();