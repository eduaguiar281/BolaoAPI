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

    }
}
