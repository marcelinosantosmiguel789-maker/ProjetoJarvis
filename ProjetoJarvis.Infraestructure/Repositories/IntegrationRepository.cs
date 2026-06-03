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
    public class IntegrationRepository : IIntegrationRepository
    {
        private readonly AppDbContext _context;
        public IntegrationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Integration integration)
        {
            await _context.Integrations.AddAsync(integration);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var integration = await _context.Integrations.FindAsync(id);
            if (integration != null)
            {
                _context.Integrations.Remove(integration);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Integration>> GetAllAsync()
        {
            return await _context.Integrations.ToListAsync();
        }
        public async Task<Integration?> GetByIdAsync(int id)
        {
            return await _context.Integrations.FindAsync(id);
        }
        public async Task UpdateAsync(Integration integration)
        {
            _context.Integrations.Update(integration);
            await _context.SaveChangesAsync();
        }
    }
}
