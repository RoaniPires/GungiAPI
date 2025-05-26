using GungiAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar Postgres
builder.Services.AddDbContext<GungiDb>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GungiDb")));

// Adiciona suporte a Controllers
builder.Services.AddControllers();

// Habilita página de exceção de banco para dev
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// (Opcional) Adiciona o Swagger para documentação automática
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// http://localhost:5267/swagger/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa roteamento para Controllers
app.MapControllers();

app.Run();