using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddFKsToCharge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Charges_CategoryId",
                table: "Charges",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_PriorityId",
                table: "Charges",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Categories_CategoryId",
                table: "Charges",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Priorities_PriorityId",
                table: "Charges",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Categories_CategoryId",
                table: "Charges");

            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Priorities_PriorityId",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_CategoryId",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_PriorityId",
                table: "Charges");
        }
    }
}
