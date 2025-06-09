using ServiceControl.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.Application.Interfaces
{
    public interface IRegistroService
    {
        Task<RegistroOutputDto> ProcessarRegistroAsync(RegistroInputDto dto);
        Task<IEnumerable<RegistroOutputDto>> ObterTodosAsync();
    }
}
