using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceJob.Migrations
{
    /// <inheritdoc />
    public partial class tablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "Binance",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "Binance",
                newName: "TradeElements",
                newSchema: "Binance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradeElements",
                schema: "Binance",
                table: "TradeElements",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeElements",
                schema: "Binance",
                table: "TradeElements");

            migrationBuilder.RenameTable(
                name: "TradeElements",
                schema: "Binance",
                newName: "User",
                newSchema: "Binance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "Binance",
                table: "User",
                column: "id");
        }
    }
}
