using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceControl.Application.Interfaces;
using ServiceControl.Application.Services;
using ServiceControl.Infrastructure.ExternalServices;
using ServiceControl.Infrastructure.Repositories;
using ServiceControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurações de serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ServiceControlDb"));

builder.Services.AddScoped<IRegistroService, RegistroService>();
builder.Services.AddScoped<IClimaService, OpenWeatherMapService>();
builder.Services.AddScoped<IRegistroRepository, RegistroRepository>();

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