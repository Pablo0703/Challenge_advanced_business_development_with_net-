using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerNotaFiscal : ControllerBase
    {
        private readonly InterfaceNotaFiscal _service;

        public ControllerNotaFiscal(InterfaceNotaFiscal service)
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
                return NotFound($"Nota fiscal com ID {id} não encontrada.");

            return Ok(dto);
        }

        [HttpGet("porNumero")]
        public async Task<IActionResult> GetByNumero([FromQuery] string numero)
        {
            var result = await _service.GetByNumeroAsync(numero);
            if (!result.Any())
                return NotFound($"Nenhuma nota fiscal encontrada com número '{numero}'.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotaFiscalDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] NotaFiscalDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL não bate com o objeto.");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Nota fiscal com ID {id} não encontrada.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Nota fiscal com ID {id} não encontrada.");

            return NoContent();
        }
    }
}
