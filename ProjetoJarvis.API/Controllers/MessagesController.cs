using Microsoft.AspNetCore.Mvc;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;

namespace ProjetoJarvis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetAll()
        {
            var messages = await _service.GetAllAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetById(int id)
        {
            var message = await _service.GetByIdAsync(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDTO>> Create(CreateMessageDTO dto)
        {
            var message = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }
    }
}