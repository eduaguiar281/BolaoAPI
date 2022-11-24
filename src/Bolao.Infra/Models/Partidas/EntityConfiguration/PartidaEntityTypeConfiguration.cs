using Bolao.Infra.Models.Partidas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bolao.Infra.Models.Partidas.EntityConfiguration
{
    internal class PartidaEntityTypeConfiguration : IEntityTypeConfiguration<Partida>
    {
        public void Configure(EntityTypeBuilder<Partida> builder)
        {
            builder.ToTable($"{nameof(Partida)}s");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Local)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.GolsAnfitriao)
                .IsRequired();

            builder.Property(p => p.Data)
                .IsRequired();

            builder.Property(p => p.GolsVisitante)
                .IsRequired();

            builder.Property(p => p.Finalizado)
                .IsRequired();

            builder.Property(p => p.Resultado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasConversion(e => e.ToString(),
                               e => (ResultadoPartida)Enum.Parse(typeof(ResultadoPartida), e));
            
            builder.HasOne(p => p.Anfitriao)
                .WithMany()
                .HasForeignKey("AnfitriaoId");
            
            builder.HasOne(p => p.Visitante)
                .WithMany()
                .HasForeignKey("VisitanteId");

            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.DataAlteracao);
            builder.Property(p => p.DataCancelamento);
            builder.Property(p => p.Ativo);
        }

    }
}
