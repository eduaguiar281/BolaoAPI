using Bolao.Infra.Models.Partidas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bolao.Infra.Models.Partidas.EntityConfiguration
{
    internal class HistoricoPartidaEntityTypeConfiguration : IEntityTypeConfiguration<HistoricoPartida>
    {
        public void Configure(EntityTypeBuilder<HistoricoPartida> builder)
        {
            builder.ToTable("HistoricosPartida");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Minuto).IsRequired(false);
            
            builder.Property(p => p.Jogador)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired(false);
            
            builder.Property(p => p.Observacoes)
                .IsUnicode(false)
                .HasMaxLength(300);

            builder.Property(p => p.Evento)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasConversion(e => e.ToString(),
                                  e => (Evento)Enum.Parse(typeof(Evento), e));

            builder.Property(p => p.Etapa)
                   .HasMaxLength(100)
                   .IsUnicode(false)
                   .HasConversion(e => e.ToString(),
                                  e => (Etapa)Enum.Parse(typeof(Etapa), e));

            builder.HasOne(p => p.Time)
                .WithMany()
                .HasForeignKey("TimeId");

            builder.HasOne(p => p.Partida)
                .WithMany(fk => fk.Historicos)
                .HasForeignKey("PartidaId");

            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.DataAlteracao);
            builder.Property(p => p.Ativo);
        }
    }
}
