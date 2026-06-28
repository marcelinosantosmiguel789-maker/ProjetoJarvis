using Microsoft.AspNetCore.Mvc;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;

namespace ProjetoJarvis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryService _service;

        public MemoryController(IMemoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemoryDTO>>> GetAll()
        {
            var memories = await _service.GetAllAsync();
            return Ok(memories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemoryDTO>> GetById(int id)
        {
            var memory = await _service.GetByIdAsync(id);
            if (memory == null)
                return NotFound();
            return Ok(memory);
        }

        [HttpGet("by-key/{key}")]
        public async Task<ActionResult<MemoryDTO>> GetByKey(string key)
        {
            var memory = await _service.GetByKeyAsync(key);
            if (memory == null)
                return NotFound();
            return Ok(memory);
        }

        [HttpPost]
        public async Task<ActionResult<MemoryDTO>> Create(CreateMemoryDTO dto)
        {
            var memory = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = memory.Id }, memory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemoryDTO dto)
        {
            dto.Id = id;
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}