using Microsoft.EntityFrameworkCore.Migrations;

namespace RpsGame_NoDb.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumLosses",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumWins",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "P1RoundWins",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "P2RoundWins",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ties",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumLosses",
                table: "players");

            migrationBuilder.DropColumn(
                name: "NumWins",
                table: "players");

            migrationBuilder.DropColumn(
                name: "P1RoundWins",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "P2RoundWins",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "Ties",
                table: "matches");
        }
    }
}
