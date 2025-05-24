using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerLocalizacaoMoto : ControllerBase
    {
        private readonly InterfaceLocalizacaoMoto _service;

        public ControllerLocalizacaoMoto(InterfaceLocalizacaoMoto service)
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
                return NotFound($"Localização de moto com ID {id} não encontrada.");
            return Ok(dto);
        }

        [HttpGet("porIdMoto")]
        public async Task<IActionResult> GetByIdMoto([FromQuery] long idMoto)
        {
            var result = await _service.GetByIdMotoAsync(idMoto);
            if (!result.Any())
                return NotFound($"Nenhuma localização encontrada para a moto com ID {idMoto}.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocalizacaoMotoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] LocalizacaoMotoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL não bate com o do objeto.");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound($"Localização de moto com ID {id} não encontrada.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Localização de moto com ID {id} não encontrada.");

            return NoContent();
        }
    }
}
