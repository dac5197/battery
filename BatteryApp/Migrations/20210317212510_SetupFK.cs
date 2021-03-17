using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class SetupFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Charges");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Charges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charges_StatusId",
                table: "Charges",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Statuses_StatusId",
                table: "Charges",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Statuses_StatusId",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_StatusId",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Charges");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Charges",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
