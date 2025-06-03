using ControleFinanceiroAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configura o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona CORS corretamente (ANTES do app.Build())
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // porta padrão do React (vite)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware CORS deve vir antes dos controladores
app.UseCors();

// Servir arquivos estáticos do React (em produção)
app.UseDefaultFiles();
app.UseStaticFiles();

// app.UseHttpsRedirection(); // descomente se estiver usando HTTPS

app.UseAuthorization();

// Roteamento da API
app.MapControllers();

// Fallback para React (SPA)
app.MapFallbackToFile("index.html");

app.Run();
