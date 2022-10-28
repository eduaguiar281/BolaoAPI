using Bolao.Security.Database.EntityConfiguration;
using Bolao.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolao.Security.Database
{
    public class UsuarioDataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public UsuarioDataContext(DbContextOptions<UsuarioDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
