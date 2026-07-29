using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foreman_Backend_Notif.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailFieldInNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "receiver_email",
                table: "notifs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receiver_email",
                table: "notifs");
        }
    }
}
