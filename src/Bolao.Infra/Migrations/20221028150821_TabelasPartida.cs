using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bolao.Infra.Migrations
{
    public partial class TabelasPartida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnfitriaoId = table.Column<int>(type: "int", nullable: true),
                    VisitanteId = table.Column<int>(type: "int", nullable: true),
                    GolsAnfitriao = table.Column<int>(type: "int", nullable: false),
                    GolsVisitante = table.Column<int>(type: "int", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    Resultado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Times_AnfitriaoId",
                        column: x => x.AnfitriaoId,
                        principalTable: "Times",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partidas_Times_VisitanteId",
                        column: x => x.VisitanteId,
                        principalTable: "Times",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricosPartida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidaId = table.Column<int>(type: "int", nullable: true),
                    TimeId = table.Column<int>(type: "int", nullable: true),
                    Minuto = table.Column<int>(type: "int", nullable: false),
                    Jogador = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Evento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Etapa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosPartida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosPartida_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosPartida_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Palpites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PartidaId = table.Column<int>(type: "int", nullable: true),
                    GolsAnfitriao = table.Column<int>(type: "int", nullable: false),
                    GolsVisitante = table.Column<int>(type: "int", nullable: false),
                    ResultadoFinal = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palpites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Palpites_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosPartida_PartidaId",
                table: "HistoricosPartida",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosPartida_TimeId",
                table: "HistoricosPartida",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Palpites_PartidaId",
                table: "Palpites",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_AnfitriaoId",
                table: "Partidas",
                column: "AnfitriaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_VisitanteId",
                table: "Partidas",
                column: "VisitanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosPartida");

            migrationBuilder.DropTable(
                name: "Palpites");

            migrationBuilder.DropTable(
                name: "Partidas");
        }
    }
}
