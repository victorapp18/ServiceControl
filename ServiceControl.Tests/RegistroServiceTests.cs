using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Services;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using Xunit;
using ServiceControl.Application.Interfaces;

namespace ServiceControl.Tests.Services
{
    public class RegistroServiceTests
    {
        private readonly Mock<IClimaService> _climaServiceMock;
        private readonly Mock<IRegistroRepository> _registroRepositoryMock;
        private readonly Mock<IServiceBClient> _serviceBClientMock;
        private readonly Mock<ILogger<RegistroService>> _loggerMock;
        private readonly RegistroService _registroService;

        public RegistroServiceTests()
        {
            _climaServiceMock = new Mock<IClimaService>();
            _registroRepositoryMock = new Mock<IRegistroRepository>();
            _serviceBClientMock = new Mock<IServiceBClient>();
            _loggerMock = new Mock<ILogger<RegistroService>>();

            _registroService = new RegistroService(
                _climaServiceMock.Object,
                _registroRepositoryMock.Object,
                _serviceBClientMock.Object,
                _loggerMock.Object
            );
        }

        [Theory]
        [InlineData(20, "OtimasCondicoes")]
        [InlineData(12, "Agradavel")]
        [InlineData(5, "Impraticavel")]
        public async Task ProcessarRegistroAsync_DeveProcessarComCondicaoCorreta(double temperatura, string condicaoEsperada)
        {
            // Arrange
            var inputDto = new RegistroInputDto
            {
                Cidade = "Belém",
                ServicoExecutado = "Fundação",
                Responsavel = "João",
                Data = DateTime.Today
            };

            _climaServiceMock
                .Setup(x => x.ObterClimaAsync("Belém"))
                .ReturnsAsync(temperatura);

            _registroRepositoryMock
                .Setup(x => x.SalvarAsync(It.IsAny<Registro>()))
                .Returns(Task.CompletedTask);

            _serviceBClientMock
                .Setup(x => x.EnviarRegistroAsync(It.IsAny<Registro>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _registroService.ProcessarRegistroAsync(inputDto);

            // Assert
            Assert.Equal("Belém", resultado.Cidade);
            Assert.Equal("Fundação", resultado.ServicoExecutado);
            Assert.Equal("João", resultado.Responsavel);
            Assert.Equal(temperatura, resultado.Temperatura);
            Assert.Equal(condicaoEsperada, resultado.CondicaoClimatica);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarTodosOsRegistros()
        {
            // Arrange
            var registros = new List<Registro>
            {
                new Registro
                {
                    Id = Guid.NewGuid(),
                    Cidade = "Ananindeua",
                    ServicoExecutado = "Terraplanagem",
                    Data = DateTime.Today,
                    Responsavel = "Victor Arthur",
                    Temperatura = 28.5,
                    CondicaoClimatica = "OtimasCondicoes"
                },
                new Registro
                {
                    Id = Guid.NewGuid(),
                    Cidade = "Marituba",
                    ServicoExecutado = "Concreto",
                    Data = DateTime.Today,
                    Responsavel = "Victor Arthur",
                    Temperatura = 10,
                    CondicaoClimatica = "Agradavel"
                }
            };

            _registroRepositoryMock
                .Setup(x => x.ObterTodosAsync())
                .ReturnsAsync(registros);

            // Act
            var resultado = await _registroService.ObterTodosAsync();

            // Assert
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, r => r.Cidade == "Ananindeua");
            Assert.Contains(resultado, r => r.Cidade == "Marituba");
        }
    }
}
