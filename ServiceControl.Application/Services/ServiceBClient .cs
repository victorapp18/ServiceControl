// ServiceControl.Application/Services/ServiceBClient.cs
using Microsoft.Extensions.Logging;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using System.Threading.Tasks;

namespace ServiceControl.Application.Services
{
    public class ServiceBClient : IServiceBClient
    {
        private readonly ILogger<ServiceBClient> _logger;

        public ServiceBClient(ILogger<ServiceBClient> logger)
        {
            _logger = logger;
        }

        public Task EnviarRegistroAsync(Registro registro)
        {
            _logger.LogInformation("Simulação: registro enviado para ServiceB — ID: {Id}", registro.Id);
            return Task.CompletedTask;
        }
    }
}
