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

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    entry.Property("DataCriacao").IsModified = false;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Property("DataAlteracao").IsModified = false;
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    entry.Property("DataCriacao").IsModified = false;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Property("DataAlteracao").IsModified = false;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
