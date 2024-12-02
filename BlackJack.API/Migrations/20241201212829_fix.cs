using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.API.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "deck",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CardDeckId",
                table: "dealer",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer",
                column: "CardDeckId",
                principalTable: "deck",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameId",
                table: "deck",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CardDeckId",
                table: "dealer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_dealer_deck_CardDeckId",
                table: "dealer",
                column: "CardDeckId",
                principalTable: "deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
