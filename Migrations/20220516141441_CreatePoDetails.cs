using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryBeginners.Migrations
{
    public partial class CreatePoDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoId = table.Column<int>(type: "int", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Quantity = table.Column<decimal>(type: "smallmoney", nullable: false),
                    Fob = table.Column<decimal>(type: "smallmoney", nullable: false),
                    PrcInBaseCur = table.Column<decimal>(type: "smallmoney", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoDetails_PoHeaders_PoId",
                        column: x => x.PoId,
                        principalTable: "PoHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoDetails_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoDetails_PoId",
                table: "PoDetails",
                column: "PoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoDetails_ProductCode",
                table: "PoDetails",
                column: "ProductCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoDetails");
        }
    }
}
