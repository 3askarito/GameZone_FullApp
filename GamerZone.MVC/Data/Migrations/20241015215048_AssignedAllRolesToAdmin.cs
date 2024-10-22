using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamerZone.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignedAllRolesToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles](UserId,RoleId) SELECT '3e784686-3067-4b94-b581-879b151bfd19', Id FROM [security].[Roles]");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = '3e784686-3067-4b94-b581-879b151bfd19'");
        }
    }
}
