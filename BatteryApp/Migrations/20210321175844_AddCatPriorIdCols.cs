using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddCatPriorIdCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Charges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Charges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Charges");
        }
    }
}
