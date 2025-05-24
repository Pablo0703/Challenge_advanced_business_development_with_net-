using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerMoto : ControllerBase
    {
        private readonly InterfaceMoto _service;

        public ControllerMoto(InterfaceMoto service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
                return NotFound($"Moto com ID {id} não encontrada.");
            return Ok(dto);
        }

        [HttpGet("porPlaca")]
        public async Task<IActionResult> GetByPlaca([FromQuery] string placa)
        {
            var result = await _service.GetByPlacaAsync(placa);
            if (!result.Any())
                return NotFound($"Nenhuma moto encontrada com placa '{placa}'.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MotoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL não bate com o objeto.");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Moto com ID {id} não encontrada.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Moto com ID {id} não encontrada.");

            return NoContent();
        }
    }
}
