using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.API.Migrations
{
    /// <inheritdoc />
    public partial class turns_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStand",
                table: "player",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerIndex",
                table: "game",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStand",
                table: "dealer",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStand",
                table: "player");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerIndex",
                table: "game");

            migrationBuilder.DropColumn(
                name: "IsStand",
                table: "dealer");
        }
    }
}
