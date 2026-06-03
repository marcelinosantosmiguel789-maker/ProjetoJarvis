using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Entities
{
    public class Message
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Message() { }

        public Message(string content, string role)
        {
            Content = content;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
