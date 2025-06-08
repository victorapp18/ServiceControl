using ServiceControl.Domain.Entities;

namespace ServiceControl.Domain.Interfaces
{
    public interface IRegistroRepository
    {
        Task SalvarAsync(Registro registro);
    }
}