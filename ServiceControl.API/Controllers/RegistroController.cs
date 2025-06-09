using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var registro = await _registroService.ProcessarRegistroAsync(dto);
            return CreatedAtAction(nameof(Post), new { id = registro.Id }, registro);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroOutputDto>>> GetAll()
        {
            var todos = await _registroService.ObterTodosAsync();
            return Ok(todos);
        }
    }
}
