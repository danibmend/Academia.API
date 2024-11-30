using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace academia.Infrastructure.Persistence.Mappings.Base
{
    internal abstract class BaseEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .HasColumnOrder(1)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.DataCriacao)
               .HasColumnName("DATA_CRIACAO");

            builder.Property(c => c.DataCriacao)
               .HasColumnName("DATA_ATUALIZACAO");

            Seed(builder);
        }

        protected abstract void ConfigureMapping(EntityTypeBuilder<TEntity> builder);

        protected virtual void Seed(EntityTypeBuilder<TEntity> builder) { }
    }
}
