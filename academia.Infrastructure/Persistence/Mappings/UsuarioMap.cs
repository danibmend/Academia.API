using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades;
using academia.Infrastructure.Persistence.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace academia.Infrastructure.Persistence.Mappings
{
    internal class UsuarioMap : BaseEntityMap<Usuario>
    {
        protected override void ConfigureMapping(EntityTypeBuilder<Usuario> builder)
        {

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Senha)
                .IsRequired()
                .HasMaxLength(30);

        }
    }
}
