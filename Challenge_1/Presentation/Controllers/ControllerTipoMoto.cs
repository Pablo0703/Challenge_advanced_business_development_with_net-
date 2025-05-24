using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMotoController : ControllerBase
    {
        private readonly InterfaceTipoMoto _service;

        public TipoMotoController(InterfaceTipoMoto service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMotoDTO>>> GetAll()
        {
            var tipos = await _service.GetAllAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoMotoDTO>> GetById(long id)
        {
            try
            {
                var tipo = await _service.GetByIdAsync(id);
                return Ok(tipo);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de moto não encontrado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TipoMotoDTO>> Create(TipoMotoDTO dto)
        {
            try
            {
                var novo = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoMotoDTO>> Update(long id, TipoMotoDTO dto)
        {
            try
            {
                var atualizado = await _service.UpdateAsync(id, dto);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de moto não encontrado.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var sucesso = await _service.DeleteAsync(id);
                if (sucesso)
                    return NoContent();

                return BadRequest("Falha ao excluir tipo de moto.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de moto não encontrado.");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
