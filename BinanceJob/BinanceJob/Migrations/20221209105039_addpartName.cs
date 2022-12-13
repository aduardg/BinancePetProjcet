using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceJob.Migrations
{
    /// <inheritdoc />
    public partial class addpartName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "namePart",
                schema: "Binance",
                table: "TradeElements",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "namePart",
                schema: "Binance",
                table: "TradeElements");
        }
    }
}
