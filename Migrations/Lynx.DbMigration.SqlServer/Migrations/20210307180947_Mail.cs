using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class Mail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Email",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: true),
                    ProcessedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Email", x => x.ID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EmailContent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Email",
                schema: "dbo");
        }
    }
}
