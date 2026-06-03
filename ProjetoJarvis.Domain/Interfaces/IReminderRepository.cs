using ProjetoJarvis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder?> GetByIdAsync(int id);
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task AddAsync(Reminder reminder);
        Task UpdateAsync(Reminder reminder);
        Task DeleteAsync(int id);
        Task<IEnumerable<Reminder>> GetPendingRemindersAsync();
        Task<IEnumerable<Reminder>> GetDueRemindersAsync();
    }
}
