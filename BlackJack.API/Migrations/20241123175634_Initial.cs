using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    Suit = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    DealerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => new { x.Suit, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "dealer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PlayersCount = table.Column<int>(type: "integer", nullable: false),
                    DealerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeckId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_game_dealer_DealerId",
                        column: x => x.DealerId,
                        principalTable: "dealer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_player_game_GameId",
                        column: x => x.GameId,
                        principalTable: "game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_card_DealerId",
                table: "card",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_card_PlayerId",
                table: "card",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_dealer_GameId",
                table: "dealer",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deck_GameId",
                table: "deck",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_DealerId",
                table: "game",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_game_DeckId",
                table: "game",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_player_GameId",
                table: "player",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_card_dealer_DealerId",
                table: "card",
                column: "DealerId",
                principalTable: "dealer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_card_player_PlayerId",
                table: "card",
                column: "PlayerId",
                principalTable: "player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dealer_game_GameId",
                table: "dealer",
                column: "GameId",
                principalTable: "game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_deck_game_GameId",
                table: "deck",
                column: "GameId",
                principalTable: "game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_dealer_DealerId",
                table: "game");

            migrationBuilder.DropForeignKey(
                name: "FK_deck_game_GameId",
                table: "deck");

            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "player");

            migrationBuilder.DropTable(
                name: "dealer");

            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "deck");
        }
    }
}
