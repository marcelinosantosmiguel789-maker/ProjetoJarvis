using Microsoft.AspNetCore.Mvc;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;

namespace ProjetoJarvis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _service;

        public ReminderController(IReminderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderDTO>>> GetAll()
        {
            var reminders = await _service.GetAllAsync();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderDTO>> GetById(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            if (reminder == null)
                return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<ActionResult<ReminderDTO>> Create(CreateReminderDTO dto)
        {
            var reminder = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = reminder.Id }, reminder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReminderDTO dto)
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

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<ReminderDTO>>> GetPending()
        {
            var reminders = await _service.GetPendingRemindersAsync();
            return Ok(reminders);
        }

        [HttpGet("due")]
        public async Task<ActionResult<IEnumerable<ReminderDTO>>> GetDue()
        {
            var reminders = await _service.GetDueRemindersAsync();
            return Ok(reminders);
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _service.CompleteReminderAsync(id);
            return NoContent();
        }
    }
}