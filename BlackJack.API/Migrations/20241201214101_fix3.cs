using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.API.Migrations
{
    /// <inheritdoc />
    public partial class fix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_dealer_DealerId",
                table: "game");

            migrationBuilder.DropForeignKey(
                name: "FK_game_deck_DeckId",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_DealerId",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_DeckId",
                table: "game");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "game");

            migrationBuilder.DropColumn(
                name: "DeckId",
                table: "game");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DealerId",
                table: "game",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeckId",
                table: "game",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_game_DealerId",
                table: "game",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_game_DeckId",
                table: "game",
                column: "DeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_game_dealer_DealerId",
                table: "game",
                column: "DealerId",
                principalTable: "dealer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_game_deck_DeckId",
                table: "game",
                column: "DeckId",
                principalTable: "deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
