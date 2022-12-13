using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceJob.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "checkColumn",
                schema: "Binance",
                table: "TradeElements",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checkColumn",
                schema: "Binance",
                table: "TradeElements");
        }
    }
}
