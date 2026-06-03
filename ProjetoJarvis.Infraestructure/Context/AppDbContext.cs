using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoJarvis.Domain.Entities;

namespace ProjetoJarvis.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}