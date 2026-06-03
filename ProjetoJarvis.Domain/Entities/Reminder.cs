using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJarvis.Domain.Entities
{
    public class Reminder
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime ExecuteAt { get; private set; }
        public bool Completed { get; private set; }

        protected Reminder() { }

        public Reminder(
            string title,
            string description,
            DateTime executeAt)
        {
            Title = title;
            Description = description;
            ExecuteAt = executeAt;
            Completed = false;
        }

        public void Complete()
        {
            Completed = true;
        }
    }
}
