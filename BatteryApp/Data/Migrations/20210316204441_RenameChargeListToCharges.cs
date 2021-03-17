using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Data.Migrations
{
    public partial class RenameChargeListToCharges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChargeList",
                table: "ChargeList");

            migrationBuilder.RenameTable(
                name: "ChargeList",
                newName: "Charges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.RenameTable(
                name: "Charges",
                newName: "ChargeList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChargeList",
                table: "ChargeList",
                column: "Id");
        }
    }
}
