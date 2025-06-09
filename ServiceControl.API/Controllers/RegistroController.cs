using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroService _registroService;

        public RegistroController(IRegistroService registroService)
        {
            _registroService = registroService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistroInputDto dto)
        {
            var resultado = await _registroService.ProcessarRegistroAsync(dto);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<RegistroOutputDto> registros = await _registroService.ObterTodosAsync();
            return Ok(registros);
        }
    }
}
