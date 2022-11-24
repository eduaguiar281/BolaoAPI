﻿// <auto-generated />
using System;
using Bolao.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bolao.Infra.Migrations
{
    [DbContext(typeof(BolaoDataContext))]
    partial class BolaoDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.HistoricoPartida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Etapa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Evento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Jogador")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("Minuto")
                        .HasColumnType("int");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<int?>("PartidaId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.HasIndex("TimeId");

                    b.ToTable("HistoricosPartida", (string)null);
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.Palpite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("GolsAnfitriao")
                        .HasColumnType("int");

                    b.Property<int>("GolsVisitante")
                        .HasColumnType("int");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("PartidaId")
                        .HasColumnType("int");

                    b.Property<string>("ResultadoFinal")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.ToTable("Palpites", (string)null);
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.Partida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AnfitriaoId")
                        .HasColumnType("int");

                    b.Property<bool?>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataCancelamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Finalizado")
                        .HasColumnType("bit");

                    b.Property<int>("GolsAnfitriao")
                        .HasColumnType("int");

                    b.Property<int>("GolsVisitante")
                        .HasColumnType("int");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Resultado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("VisitanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnfitriaoId");

                    b.HasIndex("VisitanteId");

                    b.ToTable("Partidas", (string)null);
                });

            modelBuilder.Entity("Bolao.Infra.Models.Times.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Times", (string)null);
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.HistoricoPartida", b =>
                {
                    b.HasOne("Bolao.Infra.Models.Partidas.Partida", "Partida")
                        .WithMany("Historicos")
                        .HasForeignKey("PartidaId");

                    b.HasOne("Bolao.Infra.Models.Times.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId");

                    b.Navigation("Partida");

                    b.Navigation("Time");
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.Palpite", b =>
                {
                    b.HasOne("Bolao.Infra.Models.Partidas.Partida", "Partida")
                        .WithMany("Palpites")
                        .HasForeignKey("PartidaId");

                    b.Navigation("Partida");
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.Partida", b =>
                {
                    b.HasOne("Bolao.Infra.Models.Times.Time", "Anfitriao")
                        .WithMany()
                        .HasForeignKey("AnfitriaoId");

                    b.HasOne("Bolao.Infra.Models.Times.Time", "Visitante")
                        .WithMany()
                        .HasForeignKey("VisitanteId");

                    b.Navigation("Anfitriao");

                    b.Navigation("Visitante");
                });

            modelBuilder.Entity("Bolao.Infra.Models.Partidas.Partida", b =>
                {
                    b.Navigation("Historicos");

                    b.Navigation("Palpites");
                });
#pragma warning restore 612, 618
        }
    }
}
