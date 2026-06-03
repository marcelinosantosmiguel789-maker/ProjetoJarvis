using ProjetoJarvis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Interfaces
{
    public interface IMemoryRepository
    {
        Task<Memory?> GetByIdAsync(int id);
        Task<Memory?> GetByKeyAsync(string key);
        Task<IEnumerable<Memory>> GetAllAsync();
        Task AddAsync(Memory memory);
        Task UpdateAsync(Memory memory);
        Task DeleteAsync(int id);
    }
}
