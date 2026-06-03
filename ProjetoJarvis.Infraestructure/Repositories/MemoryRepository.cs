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
    public class MemoryRepository : IMemoryRepository
    {
        private readonly AppDbContext _context;
        public MemoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Memory memory)
        {
            await _context.Memories.AddAsync(memory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var memory = await _context.Memories.FindAsync(id);
            if (memory != null)
            {
                _context.Memories.Remove(memory);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Memory>> GetAllAsync()
        {
            return await _context.Memories.ToListAsync();
        }
        public async Task<Memory?> GetByIdAsync(int id)
        {
            return await _context.Memories.FindAsync(id);
        }
        public async Task<Memory?> GetByKeyAsync(string key)
        {
            return await _context.Memories
                .FirstOrDefaultAsync(m => m.Key == key);
        }
        public async Task UpdateAsync(Memory memory)
        {
            _context.Memories.Update(memory);
            await _context.SaveChangesAsync();
        }
    }
}
