using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigScheduler_N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropColumn(
                name: "N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigSchedulerID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigScheduler_N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigSchedulerID",
                principalSchema: "dbo",
                principalTable: "tbl_ProviderTypeConfigScheduler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
