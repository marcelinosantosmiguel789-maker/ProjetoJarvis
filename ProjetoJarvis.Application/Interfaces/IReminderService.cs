using ProjetoJarvis.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IReminderService
    {
        Task<ReminderDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ReminderDTO>> GetAllAsync();
        Task<ReminderDTO> AddAsync(CreateReminderDTO dto);
        Task UpdateAsync(UpdateReminderDTO dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReminderDTO>> GetPendingRemindersAsync();
        Task<IEnumerable<ReminderDTO>> GetDueRemindersAsync();
        Task CompleteReminderAsync(int id);
    }
}
