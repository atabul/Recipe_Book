using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryBeginners.Migrations
{
    public partial class AddSelfForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExchangeCurrencyId",
                table: "Currencies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_ExchangeCurrencyId",
                table: "Currencies",
                column: "ExchangeCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Currencies_ExchangeCurrencyId",
                table: "Currencies",
                column: "ExchangeCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Currencies_ExchangeCurrencyId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_ExchangeCurrencyId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "ExchangeCurrencyId",
                table: "Currencies");
        }
    }
}
