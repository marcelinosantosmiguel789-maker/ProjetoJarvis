using ProjetoJarvis.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IMemoryService
    {
        Task<MemoryDTO?> GetByIdAsync(int id);
        Task<MemoryDTO?> GetByKeyAsync(string key);
        Task<IEnumerable<MemoryDTO>> GetAllAsync();
        Task<MemoryDTO> AddAsync(CreateMemoryDTO dto);
        Task UpdateAsync(UpdateMemoryDTO dto);
        Task DeleteAsync(int id);
    }
}
