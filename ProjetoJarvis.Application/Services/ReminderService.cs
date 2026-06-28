using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _repository;
        private readonly IMapper _mapper;

        public ReminderService(IReminderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReminderDTO?> GetByIdAsync(int id)
        {
            var reminder = await _repository.GetByIdAsync(id);
            return reminder == null ? null : _mapper.Map<ReminderDTO>(reminder);
        }

        public async Task<IEnumerable<ReminderDTO>> GetAllAsync()
        {
            var reminders = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReminderDTO>>(reminders);
        }

        public async Task<ReminderDTO> AddAsync(CreateReminderDTO dto)
        {
            var reminder = _mapper.Map<Reminder>(dto);
            await _repository.AddAsync(reminder);
            return _mapper.Map<ReminderDTO>(reminder);
        }

        public async Task UpdateAsync(UpdateReminderDTO dto)
        {
            var reminder = await _repository.GetByIdAsync(dto.Id);
            if (reminder == null)
                throw new KeyNotFoundException($"Reminder with id {dto.Id} not found.");

            var updatedReminder = _mapper.Map<Reminder>(dto);
            await _repository.UpdateAsync(updatedReminder);
        }

        public async Task DeleteAsync(int id)
        {
            var reminder = await _repository.GetByIdAsync(id);
            if (reminder == null)
                throw new KeyNotFoundException($"Reminder with id {id} not found.");

            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReminderDTO>> GetPendingRemindersAsync()
        {
            var reminders = await _repository.GetPendingRemindersAsync();
            return _mapper.Map<IEnumerable<ReminderDTO>>(reminders);
        }

        public async Task<IEnumerable<ReminderDTO>> GetDueRemindersAsync()
        {
            var reminders = await _repository.GetDueRemindersAsync();
            return _mapper.Map<IEnumerable<ReminderDTO>>(reminders);
        }

        public async Task CompleteReminderAsync(int id)
        {
            var reminder = await _repository.GetByIdAsync(id);
            if (reminder == null)
                throw new KeyNotFoundException($"Reminder with id {id} not found.");

            reminder.Complete();
            await _repository.UpdateAsync(reminder);
        }
    }
}
