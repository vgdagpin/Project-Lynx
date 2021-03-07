using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class Mail2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EmailContent",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "CC",
                schema: "dbo",
                table: "tbl_Email",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                schema: "dbo",
                table: "tbl_Email",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                schema: "dbo",
                table: "tbl_Email",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_EmailAttachment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailAttachment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailAttachment_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmailBody",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailBody", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailBody_tbl_Email_ID",
                        column: x => x.ID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmailHeader",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailHeader_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailAttachment_EmailID",
                schema: "dbo",
                table: "tbl_EmailAttachment",
                column: "EmailID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailHeader_EmailID",
                schema: "dbo",
                table: "tbl_EmailHeader",
                column: "EmailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EmailAttachment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailBody",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailHeader",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "CC",
                schema: "dbo",
                table: "tbl_Email");

            migrationBuilder.DropColumn(
                name: "Subject",
                schema: "dbo",
                table: "tbl_Email");

            migrationBuilder.DropColumn(
                name: "To",
                schema: "dbo",
                table: "tbl_Email");

            migrationBuilder.CreateTable(
                name: "tbl_EmailContent",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailContent_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailContent_EmailID",
                schema: "dbo",
                table: "tbl_EmailContent",
                column: "EmailID");
        }
    }
}
