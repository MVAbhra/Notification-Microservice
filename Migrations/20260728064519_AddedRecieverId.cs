using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foreman_Backend_Notif.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecieverId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "receiver_id",
                table: "notifs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receiver_id",
                table: "notifs");
        }
    }
}
