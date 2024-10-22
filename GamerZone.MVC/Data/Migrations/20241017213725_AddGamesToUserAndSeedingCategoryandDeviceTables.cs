using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamerZone.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesToUserAndSeedingCategoryandDeviceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGame",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGame", x => new { x.ApplicationUserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sports" },
                    { 2, "Action" },
                    { 3, "Adventure" },
                    { 4, "Racing" },
                    { 5, "Fighting" },
                    { 6, "Film" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "bi bi-playstation", "Playstation" },
                    { 2, "bi bi-xbox", "xbox" },
                    { 3, "bi bi-nintendo-switch", "Nintendo Switch" },
                    { 4, "bbi bi-pc-display", "PC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGame_GameId",
                table: "ApplicationUserGame",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGame");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
