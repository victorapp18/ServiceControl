using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using ServiceControl.Infrastructure.ExternalServices;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Adiciona configurações a partir do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configuração de serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do banco de dados em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ServiceControlDb"));

// Injeção de dependências
builder.Services.AddScoped<IRegistroService, RegistroService>();
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();

// Configurações da OpenWeatherMap
builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMap"));

// HttpClient com injeção de OpenWeatherMapService
builder.Services.AddHttpClient<IClimaService, OpenWeatherMapService>();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
