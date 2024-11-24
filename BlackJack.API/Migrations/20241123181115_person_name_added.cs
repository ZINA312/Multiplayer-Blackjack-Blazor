using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.API.Migrations
{
    /// <inheritdoc />
    public partial class person_name_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "player",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CardDeckId",
                table: "dealer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_dealer_CardDeckId",
                table: "dealer",
                column: "CardDeckId");

            migrationBuilder.AddForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer",
                column: "CardDeckId",
                principalTable: "deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer");

            migrationBuilder.DropIndex(
                name: "IX_dealer_CardDeckId",
                table: "dealer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "player");

            migrationBuilder.DropColumn(
                name: "CardDeckId",
                table: "dealer");
        }
    }
}
