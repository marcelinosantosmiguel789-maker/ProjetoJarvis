using ProjetoJarvis.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IIntegrationService
    {
        Task<IntegrationDTO?> GetByIdAsync(int id);
        Task<IEnumerable<IntegrationDTO>> GetAllAsync();
        Task<IntegrationDTO> AddAsync(CreateIntegrationDTO dto);
        Task UpdateAsync(UpdateIntegrationDTO dto);
        Task DeleteAsync(int id);
    }
}
