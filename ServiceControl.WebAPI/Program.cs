using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Habilita escuta em 0.0.0.0:80 no Docker
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

// Configuração OpenWeatherMap
builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMapSettings"));

// HttpClient com base nas configurações
builder.Services.AddHttpClient<IClimaService, WeatherService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IOptions<OpenWeatherMapSettings>>().Value;
    if (string.IsNullOrWhiteSpace(cfg.BaseUrl))
        throw new InvalidOperationException("OpenWeatherMap.BaseUrl não definido");
    client.BaseAddress = new Uri(cfg.BaseUrl);
});

// DbContext (em memória neste exemplo)
builder.Services.AddDbContextPool<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ServiceControlDb"));

// DI
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();
builder.Services.AddScoped<IServiceBClient, ServiceBClient>();
builder.Services.AddScoped<IRegistroService, RegistroService>();

// MVC e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware padrão
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
