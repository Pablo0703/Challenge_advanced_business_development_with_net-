using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerEndereco : ControllerBase
    {
        private readonly InterfaceEndereco _service;

        public ControllerEndereco(InterfaceEndereco service)
        {
            _service = service;
        }

        // GET 1: Lista todos os endereços
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET 2: Busca por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound($"Endereço com ID {id} não encontrado.");

            return Ok(dto);
        }

        // GET 3: Busca por cidade
        [HttpGet("porCidade")]
        public async Task<IActionResult> GetByCidade([FromQuery] string cidade)
        {
            var result = await _service.GetByCidadeAsync(cidade);
            if (!result.Any())
                return NotFound($"Nenhum endereço encontrado para a cidade '{cidade}'.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnderecoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] EnderecoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL não bate com o objeto.");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Endereço com ID {id} não encontrado.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Endereço com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
