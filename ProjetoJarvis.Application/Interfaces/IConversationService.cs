using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoJarvis.Application.DTOs;

namespace ProjetoJarvis.Application.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationDto?> GetByIdAsync(int id);

        Task<IEnumerable<ConversationDto>> GetAllAsync();

        Task<ConversationDto> CreateAsync();

        Task UpdateAsync(int id);

        Task DeleteAsync(int id);
    }
}
