using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bolao.Security.Migrations
{
    public partial class AddUserJogador1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "NomeCompleto", "NomeUsuario", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Roles" },
                values: new object[] { 2, "jogador1@gmail.com", "Jogador Número 1", "jogador1", "Jog@dor1", null, null, "Jogador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
