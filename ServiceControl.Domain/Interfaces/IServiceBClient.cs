// ServiceControl.Application/Interfaces/IServiceBClient.cs
using ServiceControl.Domain.Entities;
using System.Threading.Tasks;

namespace ServiceControl.Application.Interfaces
{
    public interface IServiceBClient
    {
        Task EnviarRegistroAsync(Registro registro);
    }
}
