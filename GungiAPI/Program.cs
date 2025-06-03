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

// Adiciona e configura o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **Aqui você ativa o CORS**
app.UseCors();

// Usa roteamento para Controllers
app.MapControllers();

app.Run();