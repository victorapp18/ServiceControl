using ServiceControl.Application.DTOs;

namespace ServiceControl.Application.Interfaces
{
    public interface IRegistroService
    {
        Task<RegistroOutputDto> ProcessarRegistroAsync(RegistroInputDto dto);
    }
}
