using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddChargeBatteryFKRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatteryId",
                table: "Charges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Charges_BatteryId",
                table: "Charges",
                column: "BatteryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Batteries_BatteryId",
                table: "Charges",
                column: "BatteryId",
                principalTable: "Batteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Batteries_BatteryId",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_BatteryId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "BatteryId",
                table: "Charges");
        }
    }
}
