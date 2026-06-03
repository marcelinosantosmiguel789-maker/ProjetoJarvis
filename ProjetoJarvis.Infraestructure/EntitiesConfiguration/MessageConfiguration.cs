using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoJarvis.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoJarvis.Infraestructure.EntitiesConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Role).IsRequired();
            builder.Property(m => m.CreatedAt).IsRequired();
        }
    }
}
