using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Entities
{
    public class Conversation
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<Message> Messages { get; private set; }
    }
}
