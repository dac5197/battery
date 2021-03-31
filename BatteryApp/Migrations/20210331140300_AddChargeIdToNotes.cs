using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddChargeIdToNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChargeId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ChargeId",
                table: "Notes",
                column: "ChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Charges_ChargeId",
                table: "Notes",
                column: "ChargeId",
                principalTable: "Charges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Charges_ChargeId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ChargeId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ChargeId",
                table: "Notes");
        }
    }
}
