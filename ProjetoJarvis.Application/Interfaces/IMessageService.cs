using ProjetoJarvis.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDTO?> GetByIdAsync(int id);
        Task<IEnumerable<MessageDTO>> GetAllAsync();
        Task<MessageDTO> AddAsync(CreateMessageDTO dto);
    }
}
