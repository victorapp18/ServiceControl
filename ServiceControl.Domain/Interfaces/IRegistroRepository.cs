using ServiceControl.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.Domain.Interfaces
{
    public interface IRegistroRepository
    {
        Task SalvarAsync(Registro registro);
        Task<IEnumerable<Registro>> ObterTodosAsync();
    }
}
