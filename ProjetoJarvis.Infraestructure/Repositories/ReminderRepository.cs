using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjetoJarvis.Infraestructure.Context;

namespace ProjetoJarvis.Infraestructure.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder?> GetByIdAsync(int id)
        {
            return await _context.Reminders
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reminder>> GetDueRemindersAsync()
        {
            return await _context.Reminders
                .Where(r => !r.Completed &&
                    r.ExecuteAt <= DateTime.UtcNow)
                .ToListAsync();
        }
          

        public async Task<IEnumerable<Reminder>> GetPendingRemindersAsync()
        {
            return await _context.Reminders
            .Where(r => !r.Completed)
            .ToListAsync();
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            await Task.CompletedTask;
        }
    }
}
