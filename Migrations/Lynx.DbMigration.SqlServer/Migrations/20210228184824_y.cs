using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
{
    public partial class y : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigEmail_N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigWebService_N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropColumn(
                name: "N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropColumn(
                name: "N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.AddColumn<string>(
                name: "Indentity",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                columns: new[] { "ID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                columns: new[] { "ID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ProviderTypeConfigEmail_tbl_TrackBill_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                columns: new[] { "ID", "UserID" },
                principalSchema: "dbo",
                principalTable: "tbl_TrackBill",
                principalColumns: new[] { "ID", "UserID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ProviderTypeConfigEmail_tbl_User_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                column: "UserID",
                principalSchema: "dbo",
                principalTable: "tbl_User",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ProviderTypeConfigWebService_tbl_TrackBill_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                columns: new[] { "ID", "UserID" },
                principalSchema: "dbo",
                principalTable: "tbl_TrackBill",
                principalColumns: new[] { "ID", "UserID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ProviderTypeConfigWebService_tbl_User_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                column: "UserID",
                principalSchema: "dbo",
                principalTable: "tbl_User",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ProviderTypeConfigEmail_tbl_TrackBill_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ProviderTypeConfigEmail_tbl_User_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ProviderTypeConfigWebService_tbl_TrackBill_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ProviderTypeConfigWebService_tbl_User_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail");

            migrationBuilder.DropColumn(
                name: "Indentity",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService");

            migrationBuilder.AddColumn<Guid>(
                name: "N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigEmailID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigWebServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigEmail_N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigEmailID",
                principalSchema: "dbo",
                principalTable: "tbl_ProviderTypeConfigEmail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigWebService_N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigWebServiceID",
                principalSchema: "dbo",
                principalTable: "tbl_ProviderTypeConfigWebService",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
