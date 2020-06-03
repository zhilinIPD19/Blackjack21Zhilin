using Microsoft.EntityFrameworkCore.Migrations;

namespace Blackjack21Zhilin.Migrations
{
    public partial class NotMappedPlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayCard_Player_PlayerId",
                table: "PlayCard");

            migrationBuilder.DropIndex(
                name: "IX_PlayCard_PlayerId",
                table: "PlayCard");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayCard");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId1",
                table: "PlayCard",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayCard_PlayerId1",
                table: "PlayCard",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCard_Player_PlayerId1",
                table: "PlayCard",
                column: "PlayerId1",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayCard_Player_PlayerId1",
                table: "PlayCard");

            migrationBuilder.DropIndex(
                name: "IX_PlayCard_PlayerId1",
                table: "PlayCard");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "PlayCard");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayCard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayCard_PlayerId",
                table: "PlayCard",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayCard_Player_PlayerId",
                table: "PlayCard",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
