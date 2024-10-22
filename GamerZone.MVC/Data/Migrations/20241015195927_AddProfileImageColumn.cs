using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerZone.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                schema: "security",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                schema: "security",
                table: "Users");
        }
    }
}
