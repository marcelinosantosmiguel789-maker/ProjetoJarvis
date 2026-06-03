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
    public class IntegrationConfiguration : IEntityTypeConfiguration<Integration>
    {
        public void Configure(EntityTypeBuilder<Integration> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Type).HasMaxLength(500);
            builder.Property(i => i.IsActive).IsRequired();
        }
    }
}
