using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SQLite.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tbl_User",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserLogin",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Salt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Password = table.Column<byte[]>(type: "BLOB", nullable: true),
                    IsTemporaryPassword = table.Column<bool>(type: "INTEGER", nullable: false),
                    TemporaryPassword = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserLogin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserLogin_tbl_User_ID",
                        column: x => x.ID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserSession",
                schema: "dbo",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiredOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserSession", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_tbl_UserSession_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_User",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "Admin", "Admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserLogin",
                columns: new[] { "ID", "IsTemporaryPassword", "Password", "Salt", "TemporaryPassword", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, new byte[] { 74, 226, 122, 229, 114, 200, 154, 189, 69, 226, 1, 60, 29, 91, 32, 29, 215, 153, 46, 135, 122, 208, 159, 188, 131, 1, 30, 86, 43, 221, 166, 191 }, new byte[] { 65, 68, 77, 73, 78, 45, 83, 65, 76, 84, 45, 49, 50, 51, 52, 33, 64, 35, 36 }, "k4m0t3", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLogin_Username",
                schema: "dbo",
                table: "tbl_UserLogin",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserSession_UserID",
                schema: "dbo",
                table: "tbl_UserSession",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_UserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserSession",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_User",
                schema: "dbo");
        }
    }
}
