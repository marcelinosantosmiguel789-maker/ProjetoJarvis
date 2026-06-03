using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Entities
{
    public class Integration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool IsActive { get; private set; }

        protected Integration() { }

        public Integration(string name, string type)
        {
            Name = name;
            Type = type;
            IsActive = true;
        }
    }
}
