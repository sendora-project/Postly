using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sendora.Core.Migrations
{
    /// <inheritdoc />
    public partial class Layout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    folder = table.Column<string>(type: "TEXT", nullable: false),
                    delivered_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    seen = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");
        }
    }
}
