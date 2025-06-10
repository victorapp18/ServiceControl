using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMapSettings"));

builder.Services.AddHttpClient<IClimaService, WeatherService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IOptions<OpenWeatherMapSettings>>().Value;
    if (string.IsNullOrWhiteSpace(cfg.BaseUrl))
        throw new InvalidOperationException("OpenWeatherMap.BaseUrl não definido");
    client.BaseAddress = new Uri(cfg.BaseUrl);
});

builder.Services.AddDbContextPool<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ServiceControlDb"));

builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();
builder.Services.AddScoped<IServiceBClient, ServiceBClient>();
builder.Services.AddScoped<IRegistroService, RegistroService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
