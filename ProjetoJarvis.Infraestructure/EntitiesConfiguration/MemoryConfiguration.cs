using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetoJarvis.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoJarvis.Infraestructure.EntitiesConfiguration
{
    public class MemoryConfiguration : IEntityTypeConfiguration<Memory>
    {
        public void Configure(EntityTypeBuilder<Memory> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Key).IsRequired();
            builder.Property(m => m.Value).IsRequired();
            builder.Property(m => m.CreatedAt).IsRequired();
        }
    }
}
