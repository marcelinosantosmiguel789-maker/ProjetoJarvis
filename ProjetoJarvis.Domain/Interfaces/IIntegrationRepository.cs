using ProjetoJarvis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Interfaces
{
    public interface IIntegrationRepository
    {
        Task<Integration?> GetByIdAsync(int id);
        Task<IEnumerable<Integration>> GetAllAsync();
        Task AddAsync(Integration integration);
        Task UpdateAsync(Integration integration);
        Task DeleteAsync(int id);
    }
}
