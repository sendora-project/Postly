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
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    address = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    left = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    right = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    sender_id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    subject = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    delivered_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    flags = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mails_Users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    mail_id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    recipient_category = table.Column<int>(type: "INTEGER", nullable: false),
                    MailEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recipients_Mails_MailEntityId",
                        column: x => x.MailEntityId,
                        principalTable: "Mails",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Recipients_Mails_mail_id",
                        column: x => x.mail_id,
                        principalTable: "Mails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mails_sender_id",
                table: "Mails",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_mail_id",
                table: "Recipients",
                column: "mail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_MailEntityId",
                table: "Recipients",
                column: "MailEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
