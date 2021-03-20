using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddColorToStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "Statuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FontColor",
                table: "Statuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "FontColor",
                table: "Statuses");
        }
    }
}
