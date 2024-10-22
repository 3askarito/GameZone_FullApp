using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerZone.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class bja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGame_Games_GameId",
                table: "ApplicationUserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGame_Users_ApplicationUserId",
                table: "ApplicationUserGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserGame",
                table: "ApplicationUserGame");

            migrationBuilder.RenameTable(
                name: "ApplicationUserGame",
                newName: "ApplicationUsersGames");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserGame_GameId",
                table: "ApplicationUsersGames",
                newName: "IX_ApplicationUsersGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersGames",
                table: "ApplicationUsersGames",
                columns: new[] { "ApplicationUserId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersGames_Games_GameId",
                table: "ApplicationUsersGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersGames_Users_ApplicationUserId",
                table: "ApplicationUsersGames",
                column: "ApplicationUserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersGames_Games_GameId",
                table: "ApplicationUsersGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersGames_Users_ApplicationUserId",
                table: "ApplicationUsersGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersGames",
                table: "ApplicationUsersGames");

            migrationBuilder.RenameTable(
                name: "ApplicationUsersGames",
                newName: "ApplicationUserGame");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersGames_GameId",
                table: "ApplicationUserGame",
                newName: "IX_ApplicationUserGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserGame",
                table: "ApplicationUserGame",
                columns: new[] { "ApplicationUserId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGame_Games_GameId",
                table: "ApplicationUserGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGame_Users_ApplicationUserId",
                table: "ApplicationUserGame",
                column: "ApplicationUserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
