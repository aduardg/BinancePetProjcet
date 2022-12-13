using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BinanceJob.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Binance");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Binance",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    price = table.Column<string>(type: "text", nullable: true),
                    qty = table.Column<string>(type: "text", nullable: true),
                    quoteQty = table.Column<string>(type: "text", nullable: true),
                    time = table.Column<long>(type: "bigint", nullable: false),
                    isBuyerMaker = table.Column<bool>(type: "boolean", nullable: false),
                    isBestMatch = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Binance");
        }
    }
}
