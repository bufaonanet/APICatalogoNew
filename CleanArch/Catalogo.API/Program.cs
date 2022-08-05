using Catalogo.CrossCutting.IoC;
using Catalogo.Domain.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureApi(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

SeedUserRoles(app);

app.MapControllers();

app.Run();

void SeedUserRoles(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var seed = serviceScope.ServiceProvider
                               .GetService<ISeedUserRoleInitial>();
        seed.SeedRoles();
        seed.SeedUsers();
    }
}
