using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Enums;
using ServiceControl.Domain.Interfaces;
using Microsoft.Win32;

namespace ServiceControl.Application.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly IClimaService _climaService;
        private readonly IRegistroRepository _repository;

        public RegistroService(IClimaService climaService, IRegistroRepository repository)
        {
            _climaService = climaService;
            _repository = repository;
        }

        public async Task<RegistroOutputDto> ProcessarRegistroAsync(RegistroInputDto dto)
        {
            var clima = await _climaService.ObterClimaAsync(dto.Cidade);

            var condicao = clima switch
            {
                >= 15 and <= 30 => CondicaoClimatica.OtimasCondicoes,
                >= 10 and < 15 => CondicaoClimatica.Agradavel,
                _ => CondicaoClimatica.Impraticavel
            };

            var registro = new Registro
            {
                Id = dto.Id,
                ServicoExecutado = dto.ServicoExecutado,
                Data = dto.Data,
                Responsavel = dto.Responsavel,
                Temperatura = clima,
                CondicaoClimatica = condicao
            };

            await _repository.SalvarAsync(registro);

            return new RegistroOutputDto
            {
                Id = registro.Id,
                ServicoExecutado = registro.ServicoExecutado,
                Data = registro.Data,
                Responsavel = registro.Responsavel,
                Temperatura = registro.Temperatura,
                CondicaoClimatica = registro.CondicaoClimatica.ToString()
            };
        }
    }
}