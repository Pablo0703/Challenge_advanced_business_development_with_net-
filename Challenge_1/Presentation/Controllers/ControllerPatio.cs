using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerPatio : ControllerBase
    {
        private readonly InterfacePatio _service;

        public ControllerPatio(InterfacePatio service)
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
                return NotFound($"Pátio com ID {id} não encontrado.");
            return Ok(dto);
        }

        [HttpGet("porNome")]
        public async Task<IActionResult> GetByNome([FromQuery] string nome)
        {
            var result = await _service.GetByNomeAsync(nome);
            if (!result.Any())
                return NotFound($"Nenhum pátio encontrado com nome '{nome}'.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] PatioDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL não bate com o objeto.");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Pátio com ID {id} não encontrado.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Pátio com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
