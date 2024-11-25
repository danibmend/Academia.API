using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace academia.Infrastructure.Persistence.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id); 
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired();

            builder.Property(c => c.Senha)
                .IsRequired();

        }
    }
}
