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
                name: "tbl_Bill",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssemblyName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bill", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NotificationTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TemplateCode = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NotificationTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProviderType",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<short>(type: "INTEGER", nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderType", x => x.ID);
                });

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
                name: "tbl_BillSetting",
                schema: "dbo",
                columns: table => new
                {
                    BillID = table.Column<short>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BillSetting", x => new { x.BillID, x.Code });
                    table.ForeignKey(
                        name: "FK_tbl_BillSetting_tbl_Bill_BillID",
                        column: x => x.BillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BillProvider",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillID = table.Column<short>(type: "INTEGER", nullable: false),
                    ProviderTypeID = table.Column<short>(type: "INTEGER", nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BillProvider", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_BillProvider_tbl_Bill_BillID",
                        column: x => x.BillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BillProvider_tbl_ProviderType_ProviderTypeID",
                        column: x => x.ProviderTypeID,
                        principalSchema: "dbo",
                        principalTable: "tbl_ProviderType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackBill",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    BillID = table.Column<short>(type: "INTEGER", nullable: false),
                    ProviderTypeID = table.Column<short>(type: "INTEGER", nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackBill", x => new { x.ID, x.UserID });
                    table.ForeignKey(
                        name: "FK_tbl_TrackBill_tbl_Bill_BillID",
                        column: x => x.BillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBill_tbl_ProviderType_ProviderTypeID",
                        column: x => x.ProviderTypeID,
                        principalSchema: "dbo",
                        principalTable: "tbl_ProviderType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBill_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "tbl_UserNotification",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    IsOpened = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReceivedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserNotification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserNotification_tbl_User_UserID",
                        column: x => x.UserID,
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

            migrationBuilder.CreateTable(
                name: "tbl_NotificationConfiguration",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserBillTrackingID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsScheduled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    N_UserBillTrackingID = table.Column<Guid>(type: "TEXT", nullable: true),
                    N_UserBillTrackingUserID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NotificationConfiguration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_NotificationConfiguration_tbl_TrackBill_N_UserBillTrackingID_N_UserBillTrackingUserID",
                        columns: x => new { x.N_UserBillTrackingID, x.N_UserBillTrackingUserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackBillScheduler",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackBillID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ShortDesc = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    DayFrequency = table.Column<short>(type: "INTEGER", nullable: true),
                    SkipTimes = table.Column<short>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackBillScheduler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBillScheduler_tbl_TrackBill_TrackBillID_UserID",
                        columns: x => new { x.TrackBillID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBillScheduler_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackBillSetting",
                schema: "dbo",
                columns: table => new
                {
                    TrackBillID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackBillSetting", x => new { x.TrackBillID, x.UserID, x.Code });
                    table.ForeignKey(
                        name: "FK_tbl_TrackBillSetting_tbl_TrackBill_TrackBillID_UserID",
                        columns: x => new { x.TrackBillID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBill",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackBillID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserBill_tbl_TrackBill_TrackBillID_UserID",
                        columns: x => new { x.TrackBillID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UserBill_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tbl_SchedulerEntry",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackBillSchedulerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    IsGenerated = table.Column<bool>(type: "INTEGER", nullable: true),
                    GeneratedUserBillID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SchedulerEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_SchedulerEntry_tbl_TrackBillScheduler_TrackBillSchedulerID",
                        column: x => x.TrackBillSchedulerID,
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBillScheduler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBillAttachment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserBillID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBillAttachment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserBillAttachment_tbl_UserBill_UserBillID",
                        column: x => x.UserBillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_UserBill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBillPayment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserBillID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    PaidOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBillPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserBillPayment_tbl_UserBill_UserBillID",
                        column: x => x.UserBillID,
                        principalSchema: "dbo",
                        principalTable: "tbl_UserBill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBillPaymentTransaction",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserBillPaymentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBillPaymentTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserBillPaymentTransaction_tbl_UserBillPayment_UserBillPaymentID",
                        column: x => x.UserBillPaymentID,
                        principalSchema: "dbo",
                        principalTable: "tbl_UserBillPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_Bill",
                columns: new[] { "ID", "AssemblyName", "Code", "IsEnabled", "LongDesc", "ShortDesc", "TypeName" },
                values: new object[] { (short)1, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Globe", true, "Globe", "Globe", "Lynx.Commands.BillCmds.GlobeBillCmd" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_Bill",
                columns: new[] { "ID", "AssemblyName", "Code", "IsEnabled", "LongDesc", "ShortDesc", "TypeName" },
                values: new object[] { (short)2, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Meralco", true, "Meralco", "Meralco", "Lynx.Commands.BillCmds.MeralcoBillCmd" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_Bill",
                columns: new[] { "ID", "AssemblyName", "Code", "IsEnabled", "LongDesc", "ShortDesc", "TypeName" },
                values: new object[] { (short)3, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "House Loan Amortization", true, "House Loan Amortization", "House Loan Amortization", "Lynx.Commands.BillCmds.HouseLoanAmortizationCmd" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_Bill",
                columns: new[] { "ID", "AssemblyName", "Code", "IsEnabled", "LongDesc", "ShortDesc", "TypeName" },
                values: new object[] { (short)4, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Car Loan Amortization", true, "Car Loan Amortization", "Car Loan Amortization", "Lynx.Commands.BillCmds.CarLoanAmortizationCmd" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_ProviderType",
                columns: new[] { "ID", "LongDesc", "ShortDesc" },
                values: new object[] { (short)1, "Scheduled", "Scheduled" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_ProviderType",
                columns: new[] { "ID", "LongDesc", "ShortDesc" },
                values: new object[] { (short)2, "API", "API" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_ProviderType",
                columns: new[] { "ID", "LongDesc", "ShortDesc" },
                values: new object[] { (short)3, "Email", "Email" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_User",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "Admin", "Admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_User",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), "Vincent", "Dagpin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillProvider",
                columns: new[] { "ID", "BillID", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { (short)3, (short)3, null, (short)1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillProvider",
                columns: new[] { "ID", "BillID", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { (short)4, (short)4, null, (short)1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillProvider",
                columns: new[] { "ID", "BillID", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { (short)1, (short)1, null, (short)3, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillProvider",
                columns: new[] { "ID", "BillID", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { (short)2, (short)2, null, (short)3, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), "GLOBE-ACCT-NUM", (short)1, true, null, (short)3, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), "MERALCO-ACCT-NUM", (short)2, true, null, (short)3, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002"), "LANCASTER-ACCT-NUM", (short)3, true, null, (short)1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002"), "588-ACCT-NUM", (short)3, true, null, (short)1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002"), "BRV-ACCT-NUM", (short)4, true, null, (short)1, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserLogin",
                columns: new[] { "ID", "IsTemporaryPassword", "Password", "Salt", "TemporaryPassword", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, new byte[] { 217, 202, 150, 25, 117, 221, 121, 108, 37, 105, 80, 196, 244, 39, 65, 219, 215, 163, 50, 198, 193, 228, 162, 123, 181, 60, 246, 155, 163, 199, 37, 50 }, new byte[] { 48, 48, 48, 48, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 49 }, "k4m0t3", "admin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserLogin",
                columns: new[] { "ID", "IsTemporaryPassword", "Password", "Salt", "TemporaryPassword", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), true, new byte[] { 9, 238, 26, 39, 21, 0, 163, 101, 247, 77, 240, 232, 43, 49, 198, 12, 240, 157, 218, 92, 107, 1, 52, 133, 11, 6, 230, 247, 114, 49, 127, 182 }, new byte[] { 48, 48, 48, 48, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 50 }, "k4m0t3", "vgdagpin" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBillScheduler",
                columns: new[] { "ID", "Amount", "DayFrequency", "EndDate", "Frequency", "LongDesc", "ShortDesc", "SkipTimes", "StartDate", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), 21000m, (short)23, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBillScheduler",
                columns: new[] { "ID", "Amount", "DayFrequency", "EndDate", "Frequency", "LongDesc", "ShortDesc", "SkipTimes", "StartDate", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), 14000m, (short)28, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBillScheduler",
                columns: new[] { "ID", "Amount", "DayFrequency", "EndDate", "Frequency", "LongDesc", "ShortDesc", "SkipTimes", "StartDate", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), 15000m, (short)13, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "ID", "Amount", "DueDate", "Status", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 2100m, new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "ID", "Amount", "DueDate", "Status", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), 2100m, new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "ID", "Amount", "DueDate", "Status", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), 14000m, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "ID", "Amount", "DueDate", "Status", "TrackBillID", "UserID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), 15000m, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 21000m, new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002"), true, null, new Guid("00000000-0000-0000-0000-000000000003") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), 21000m, new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), 21000m, new DateTime(2021, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), 21000m, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), 14000m, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003"), true, null, new Guid("00000000-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000006"), 14000m, new DateTime(2021, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000007"), 14000m, new DateTime(2021, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), 14000m, new DateTime(2021, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000009"), 15000m, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004"), true, null, new Guid("00000000-0000-0000-0000-000000000005") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-00000000000a"), 15000m, new DateTime(2021, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-00000000000b"), 15000m, new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-00000000000c"), 15000m, new DateTime(2021, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bill_Code",
                schema: "dbo",
                table: "tbl_Bill",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BillProvider_BillID",
                schema: "dbo",
                table: "tbl_BillProvider",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BillProvider_ProviderTypeID",
                schema: "dbo",
                table: "tbl_BillProvider",
                column: "ProviderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_NotificationConfiguration_N_UserBillTrackingID_N_UserBillTrackingUserID",
                schema: "dbo",
                table: "tbl_NotificationConfiguration",
                columns: new[] { "N_UserBillTrackingID", "N_UserBillTrackingUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SchedulerEntry_TrackBillSchedulerID",
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                column: "TrackBillSchedulerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_BillID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_ProviderTypeID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "ProviderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_UserID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBillScheduler_TrackBillID_UserID",
                schema: "dbo",
                table: "tbl_TrackBillScheduler",
                columns: new[] { "TrackBillID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBillScheduler_UserID",
                schema: "dbo",
                table: "tbl_TrackBillScheduler",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBill_TrackBillID_UserID",
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "TrackBillID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBill_UserID",
                schema: "dbo",
                table: "tbl_UserBill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBillAttachment_UserBillID",
                schema: "dbo",
                table: "tbl_UserBillAttachment",
                column: "UserBillID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBillPayment_UserBillID",
                schema: "dbo",
                table: "tbl_UserBillPayment",
                column: "UserBillID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBillPaymentTransaction_UserBillPaymentID",
                schema: "dbo",
                table: "tbl_UserBillPaymentTransaction",
                column: "UserBillPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserLogin_Username",
                schema: "dbo",
                table: "tbl_UserLogin",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserNotification_UserID",
                schema: "dbo",
                table: "tbl_UserNotification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserSession_UserID",
                schema: "dbo",
                table: "tbl_UserSession",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BillProvider",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_BillSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_NotificationConfiguration",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_NotificationTemplate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_SchedulerEntry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_TrackBillSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBillAttachment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBillPaymentTransaction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserNotification",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserSession",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_TrackBillScheduler",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBillPayment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_TrackBill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Bill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_User",
                schema: "dbo");
        }
    }
}
