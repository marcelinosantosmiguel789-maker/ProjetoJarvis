using Microsoft.AspNetCore.Mvc;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;

namespace ProjetoJarvis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationService _service;

        public IntegrationController(IIntegrationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntegrationDTO>>> GetAll()
        {
            var integrations = await _service.GetAllAsync();
            return Ok(integrations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IntegrationDTO>> GetById(int id)
        {
            var integration = await _service.GetByIdAsync(id);
            if (integration == null)
                return NotFound();
            return Ok(integration);
        }

        [HttpPost]
        public async Task<ActionResult<IntegrationDTO>> Create(CreateIntegrationDTO dto)
        {
            var integration = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = integration.Id }, integration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateIntegrationDTO dto)
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
