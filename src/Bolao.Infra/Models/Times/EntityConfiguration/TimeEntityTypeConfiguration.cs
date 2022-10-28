using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bolao.Infra.Models.Times.EntityConfiguration
{
    internal class TimeEntityTypeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable($"{nameof(Time)}s");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .IsRequired(true);
        }
    }
}
