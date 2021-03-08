using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class MailPartType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EmailHeader",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "tbl_EmailPart",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(type: "bigint", nullable: false),
                    PartType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailPart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailPart_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailPart_EmailID",
                schema: "dbo",
                table: "tbl_EmailPart",
                column: "EmailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EmailPart",
                schema: "dbo");

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
                name: "IX_tbl_EmailHeader_EmailID",
                schema: "dbo",
                table: "tbl_EmailHeader",
                column: "EmailID");
        }
    }
}
