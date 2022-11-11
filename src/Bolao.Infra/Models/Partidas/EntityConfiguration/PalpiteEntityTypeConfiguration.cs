using Bolao.Infra.Models.Partidas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bolao.Infra.Models.Partidas.EntityConfiguration
{
    internal class PalpiteEntityTypeConfiguration : IEntityTypeConfiguration<Palpite>
    {
        public void Configure(EntityTypeBuilder<Palpite> builder)
        {
            builder.ToTable($"{nameof(Palpite)}s");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.NomeUsuario)
                .IsUnicode(false)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(p => p.Partida)
                .WithMany(p => p.Palpites)
                .HasForeignKey("PartidaId");

            builder.Property(p => p.GolsVisitante)
                .IsRequired();

            builder.Property(p => p.GolsAnfitriao)
                .IsRequired();

            builder.Property(p => p.ResultadoFinal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasConversion(e => e.ToString(),
                               e => (ResultadoPartida)Enum.Parse(typeof(ResultadoPartida), e));
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.DataAlteracao);
            builder.Property(p => p.Ativo);
        }
    }
}
