using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades;
using academia.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace academia.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
