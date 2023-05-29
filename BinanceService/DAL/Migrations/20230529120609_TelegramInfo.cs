using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class TelegramInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TelegramId",
                schema: "Admin",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TelegramInfo",
                schema: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    ChatId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInfo", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TelegramId",
                schema: "Admin",
                table: "AspNetUsers",
                column: "TelegramId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TelegramInfo_TelegramId",
                schema: "Admin",
                table: "AspNetUsers",
                column: "TelegramId",
                principalSchema: "Admin",
                principalTable: "TelegramInfo",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TelegramInfo_TelegramId",
                schema: "Admin",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TelegramInfo",
                schema: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TelegramId",
                schema: "Admin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TelegramId",
                schema: "Admin",
                table: "AspNetUsers");
        }
    }
}
