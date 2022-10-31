using Bolao.Infra.Models.Partidas.EntityConfiguration;
using Bolao.Infra.Models.Times;
using Bolao.Infra.Models.Times.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Bolao.Infra.Database
{
    public class BolaoDataContext : DbContext
    {
        public BolaoDataContext(DbContextOptions<BolaoDataContext> options) : base(options)
        {

        }
        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TimeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HistoricoPartidaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PartidaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PalpiteEntityTypeConfiguration());
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
