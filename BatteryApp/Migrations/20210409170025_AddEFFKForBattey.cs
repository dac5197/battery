using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddEFFKForBattey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Statuses_BatteryId",
                table: "Statuses",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_BatteryId",
                table: "Priorities",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BatteryId",
                table: "Categories",
                column: "BatteryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Batteries_BatteryId",
                table: "Categories",
                column: "BatteryId",
                principalTable: "Batteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Batteries_BatteryId",
                table: "Priorities",
                column: "BatteryId",
                principalTable: "Batteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Batteries_BatteryId",
                table: "Statuses",
                column: "BatteryId",
                principalTable: "Batteries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Batteries_BatteryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Batteries_BatteryId",
                table: "Priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Batteries_BatteryId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_BatteryId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_BatteryId",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BatteryId",
                table: "Categories");
        }
    }
}
