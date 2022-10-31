using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bolao.Infra.Migrations
{
    public partial class DadosEntidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Times",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Times",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Times",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Partidas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Partidas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Partidas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Palpites",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Palpites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Palpites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "HistoricosPartida",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "HistoricosPartida",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "HistoricosPartida",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Partidas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Palpites");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Palpites");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Palpites");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "HistoricosPartida");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "HistoricosPartida");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "HistoricosPartida");
        }
    }
}
