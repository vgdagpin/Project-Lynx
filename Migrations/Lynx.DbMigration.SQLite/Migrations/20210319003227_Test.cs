using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SQLite.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BillPaymentStepsTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillID = table.Column<short>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    LongDesc = table.Column<string>(type: "TEXT", nullable: false),
                    Keywords = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "tbl_TextTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RecordStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TextTemplate", x => x.ID);
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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TextTemplate",
                columns: new[] { "ID", "Code", "Content", "RecordStatus", "Title", "UpdatedOn", "Version" },
                values: new object[] { 1, "PayWithLynxTOS", "Test", "Active", "Pay with Lynx Terms of Service", new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1.0" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BillPaymentStepsTemplate_BillID",
                schema: "dbo",
                table: "tbl_BillPaymentStepsTemplate",
                column: "BillID");

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
                name: "tbl_BillPaymentStepsTemplate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_TextTemplate",
                schema: "dbo");
        }
    }
}
