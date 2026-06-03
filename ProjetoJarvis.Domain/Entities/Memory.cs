using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Entities
{
    public class Memory
    {
        public int Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Memory() { }

        public Memory(string key, string value)
        {
            Key = key;
            Value = value;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
