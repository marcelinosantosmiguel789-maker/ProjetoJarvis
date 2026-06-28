using Microsoft.AspNetCore.Mvc;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;

namespace ProjetoJarvis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _service;

        public ConversationController(IConversationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationDto>>> GetAll()
        {
            var conversations = await _service.GetAllAsync();
            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDto>> GetById(int id)
        {
            var conversation = await _service.GetByIdAsync(id);
            if (conversation == null)
                return NotFound();
            return Ok(conversation);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDto>> Create()
        {
            var conversation = await _service.CreateAsync();
            return CreatedAtAction(nameof(GetById), new { id = conversation.Id }, conversation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            await _service.UpdateAsync(id);
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