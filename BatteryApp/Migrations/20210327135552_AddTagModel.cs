using Microsoft.EntityFrameworkCore.Migrations;

namespace BatteryApp.Migrations
{
    public partial class AddTagModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BatteryId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Batteries_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargeTagRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChargeId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeTagRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeTagRelations_Charges_ChargeId",
                        column: x => x.ChargeId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChargeTagRelations_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeTagRelations_ChargeId",
                table: "ChargeTagRelations",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeTagRelations_TagId",
                table: "ChargeTagRelations",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BatteryId",
                table: "Tags",
                column: "BatteryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeTagRelations");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
