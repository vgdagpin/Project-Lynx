using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class MailBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                schema: "dbo",
                table: "tbl_EmailBody",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "Html",
                schema: "dbo",
                table: "tbl_EmailBody",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Raw",
                schema: "dbo",
                table: "tbl_EmailBody",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Html",
                schema: "dbo",
                table: "tbl_EmailBody");

            migrationBuilder.DropColumn(
                name: "Raw",
                schema: "dbo",
                table: "tbl_EmailBody");

            migrationBuilder.RenameColumn(
                name: "Text",
                schema: "dbo",
                table: "tbl_EmailBody",
                newName: "Content");
        }
    }
}
