using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class TextTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_TextTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecordStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TextTemplate", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TextTemplate",
                columns: new[] { "ID", "Code", "Content", "RecordStatus", "Title", "UpdatedOn", "Version" },
                values: new object[] { 1, "PayWithLynxTOS", "Test", "Active", "Pay with Lynx Terms of Service", new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1.0" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TextTemplate_Code_RecordStatus",
                schema: "dbo",
                table: "tbl_TextTemplate",
                columns: new[] { "Code", "RecordStatus" },
                unique: true,
                filter: "[RecordStatus] = 'Active'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_TextTemplate",
                schema: "dbo");
        }
    }
}
