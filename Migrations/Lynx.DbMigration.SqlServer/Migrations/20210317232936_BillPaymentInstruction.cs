using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class BillPaymentInstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BillPaymentStepsTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<short>(type: "smallint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LongDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BillPaymentStepsTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_BillPaymentStepsTemplate_tbl_Bill_BillID",
                        column: x => x.BillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillPaymentStepsTemplate",
                columns: new[] { "ID", "BillID", "Keywords", "LongDesc", "ShortDesc", "Title" },
                values: new object[] { 1, (short)3, "7/11,711", "Just kidding, see below very long instructions", "Go to nearest 7/11, ask cashier for instructions lol", "7/11" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillPaymentStepsTemplate",
                columns: new[] { "ID", "BillID", "Keywords", "LongDesc", "ShortDesc", "Title" },
                values: new object[] { 2, (short)3, "e-wallet,ewallet,wallet,gcash", "See below very long instructions", "Payment using GCash, follow this instruction", "GCash" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillPaymentStepsTemplate",
                columns: new[] { "ID", "BillID", "Keywords", "LongDesc", "ShortDesc", "Title" },
                values: new object[] { 3, (short)3, "e-wallet,ewallet,wallet,paymaya", "See below very long instructions", "Payment using Paymaya, follow this instruction", "Paymaya" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BillPaymentStepsTemplate_BillID",
                schema: "dbo",
                table: "tbl_BillPaymentStepsTemplate",
                column: "BillID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BillPaymentStepsTemplate",
                schema: "dbo");
        }
    }
}
