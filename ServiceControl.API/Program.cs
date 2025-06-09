using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Settings;
using System;

var builder = WebApplication.CreateBuilder(args);

// 1) Faz o bind de OpenWeatherMapSettings
builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMapSettings"));

// 2) Registra o HttpClient para IClimaService → WeatherService
//    Dentro do callback, usamos IOptions<OpenWeatherMapSettings> para ler ApiKey e BaseUrl
builder.Services.AddHttpClient<IClimaService, WeatherService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<OpenWeatherMapSettings>>().Value;

    // Garante que o BaseUrl tenha sido definido no appsettings.json
    if (string.IsNullOrWhiteSpace(settings.BaseUrl))
    {
        throw new InvalidOperationException(
            "OpenWeatherMapSettings:BaseUrl não pode estar em branco. " +
            "Verifique seu appsettings.json em ServiceControl.API."
        );
    }

    client.BaseAddress = new Uri(settings.BaseUrl);
});

// 3) Registra o DbContext (InMemory) e o repositório de Registro
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ServiceControlDb"));
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();

// 4) Registra o serviço de domínio
builder.Services.AddScoped<IRegistroService, RegistroService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
