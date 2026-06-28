using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Application.DTOs
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
