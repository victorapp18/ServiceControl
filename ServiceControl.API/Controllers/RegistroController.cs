using Microsoft.AspNetCore.Mvc;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;

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
            var result = await _registroService.ProcessarRegistroAsync(dto);
            return Ok(result);
        }
    }
}
