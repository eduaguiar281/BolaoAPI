using Bolao.Security.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Bolao.Security.Database.EntityConfiguration
{
    internal class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable($"{nameof(Usuario)}s");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.NomeUsuario)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.NomeCompleto)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Email)
                   .HasMaxLength(40)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.Password)
                   .HasMaxLength(25)
                   .IsUnicode(false)
                   .IsRequired(true);

            builder.Property(p => p.RefreshToken)
                   .HasMaxLength(300)
                   .IsUnicode(false)
                   .IsRequired(false);

            builder.Property(p => p.Roles)
                   .HasMaxLength(1000)
                   .IsUnicode(false)
                   .IsRequired(false);

            builder.Property(p => p.RefreshTokenExpiryTime)
                   .IsRequired(false);

            builder.HasData(new Usuario
            {
                Id = 1,
                Email = "tioaguiar@gmail.com",
                NomeCompleto = "Eduardo Rodrigues de Aguiar",
                NomeUsuario = "tioaguiar",
                Password = "Ti0@guiar",
                Roles = "Admin"
            },
            new Usuario
            {
                Id = 2,
                Email = "jogador1@gmail.com",
                NomeCompleto = "Jogador Número 1",
                NomeUsuario = "jogador1",
                Password = "Jog@dor1",
                Roles = "Jogador"
            });
        }
    }
}
