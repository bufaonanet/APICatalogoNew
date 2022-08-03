using Catalogo.WebUI.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("CategoriasApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CategoriasApi"]);
});
builder.Services.AddHttpClient("ProdutosApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProdutosApi"]);
});
builder.Services.AddHttpClient("Autenticapi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:Autenticapi"]);
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));    
});

builder.Services.AddScoped<ICategoriaServie, CategoriaServie>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
