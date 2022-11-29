using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bolao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CampoEtapaDeHistoricoParaPartida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "HistoricosPartida");

            migrationBuilder.AddColumn<string>(
                name: "Etapa",
                table: "Partidas",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "Partidas");

            migrationBuilder.AddColumn<string>(
                name: "Etapa",
                table: "HistoricosPartida",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
