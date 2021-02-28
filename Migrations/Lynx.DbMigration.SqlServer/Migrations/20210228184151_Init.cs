using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lynx.DbMigration.SqlServer.Migrations
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
                    ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ID = table.Column<short>(type: "smallint", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProviderTypeConfigEmail",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigEmail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProviderTypeConfigWebService",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigWebService", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    BillID = table.Column<short>(type: "smallint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
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
                    ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<short>(type: "smallint", nullable: false),
                    ProviderTypeID = table.Column<short>(type: "smallint", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                name: "tbl_UserLogin",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsTemporaryPassword = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpened = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    SessionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false, computedColumnSql: "CONVERT(BIT, (IIF(ExpiredOn IS NOT NULL AND GETUTCDATE() >= ExpiredOn, 1, 0)))"),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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
                name: "tbl_TrackBill",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<short>(type: "smallint", nullable: false),
                    ProviderTypeID = table.Column<short>(type: "smallint", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    N_ProviderTypeConfigEmailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    N_ProviderTypeConfigSchedulerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    N_ProviderTypeConfigWebServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigEmail_N_ProviderTypeConfigEmailID",
                        column: x => x.N_ProviderTypeConfigEmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_ProviderTypeConfigEmail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBill_tbl_ProviderTypeConfigWebService_N_ProviderTypeConfigWebServiceID",
                        column: x => x.N_ProviderTypeConfigWebServiceID,
                        principalSchema: "dbo",
                        principalTable: "tbl_ProviderTypeConfigWebService",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_TrackBill_tbl_User_UserID",
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBillTrackingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsScheduled = table.Column<bool>(type: "bit", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    N_UserBillTrackingID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    N_UserBillTrackingUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "tbl_ProviderTypeConfigScheduler",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DayFrequency = table.Column<short>(type: "smallint", nullable: true),
                    SkipTimes = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigScheduler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigScheduler_tbl_TrackBill_ID_UserID",
                        columns: x => new { x.ID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigScheduler_tbl_User_UserID",
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
                    TrackBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackBillSchedulerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    IsGenerated = table.Column<bool>(type: "bit", nullable: true),
                    GeneratedUserBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SchedulerEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_SchedulerEntry_tbl_ProviderTypeConfigScheduler_TrackBillSchedulerID",
                        column: x => x.TrackBillSchedulerID,
                        principalSchema: "dbo",
                        principalTable: "tbl_ProviderTypeConfigScheduler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBillAttachment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBillPaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                values: new object[,]
                {
                    { (short)1, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Globe", true, "Globe", "Globe", "Lynx.Commands.BillCmds.GlobeBillCmd" },
                    { (short)2, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Meralco", true, "Meralco", "Meralco", "Lynx.Commands.BillCmds.MeralcoBillCmd" },
                    { (short)3, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "House Loan Amortization", true, "House Loan Amortization", "House Loan Amortization", "Lynx.Commands.BillCmds.HouseLoanAmortizationCmd" },
                    { (short)4, "Lynx, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Car Loan Amortization", true, "Car Loan Amortization", "Car Loan Amortization", "Lynx.Commands.BillCmds.CarLoanAmortizationCmd" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_ProviderType",
                columns: new[] { "ID", "LongDesc", "ShortDesc" },
                values: new object[,]
                {
                    { (short)1, "Scheduled", "Scheduled" },
                    { (short)2, "API", "API" },
                    { (short)3, "Email", "Email" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_User",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Admin", "Admin" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Vincent", "Dagpin" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_BillProvider",
                columns: new[] { "ID", "BillID", "LongDesc", "ProviderTypeID", "ShortDesc" },
                values: new object[,]
                {
                    { (short)3, (short)3, null, (short)1, null },
                    { (short)4, (short)4, null, (short)1, null },
                    { (short)1, (short)1, null, (short)3, null },
                    { (short)2, (short)2, null, (short)3, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_TrackBill",
                columns: new[] { "ID", "UserID", "AccountNumber", "BillID", "IsEnabled", "LongDesc", "N_ProviderTypeConfigEmailID", "N_ProviderTypeConfigSchedulerID", "N_ProviderTypeConfigWebServiceID", "ProviderTypeID", "ShortDesc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), "GLOBE-ACCT-NUM", (short)1, true, null, null, null, null, (short)3, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), "MERALCO-ACCT-NUM", (short)2, true, null, null, null, null, (short)3, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002"), "LANCASTER-ACCT-NUM", (short)3, true, null, null, null, null, (short)1, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002"), "588-ACCT-NUM", (short)3, true, null, null, null, null, (short)1, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002"), "BRV-ACCT-NUM", (short)4, true, null, null, null, null, (short)1, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserLogin",
                columns: new[] { "ID", "IsTemporaryPassword", "Password", "Salt", "TemporaryPassword", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), true, new byte[] { 217, 202, 150, 25, 117, 221, 121, 108, 37, 105, 80, 196, 244, 39, 65, 219, 215, 163, 50, 198, 193, 228, 162, 123, 181, 60, 246, 155, 163, 199, 37, 50 }, new byte[] { 48, 48, 48, 48, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 49 }, "k4m0t3", "admin" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), true, new byte[] { 9, 238, 26, 39, 21, 0, 163, 101, 247, 77, 240, 232, 43, 49, 198, 12, 240, 157, 218, 92, 107, 1, 52, 133, 11, 6, 230, 247, 114, 49, 127, 182 }, new byte[] { 48, 48, 48, 48, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 45, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 50 }, "k4m0t3", "vgdagpin" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler",
                columns: new[] { "ID", "Amount", "DayFrequency", "EndDate", "Frequency", "LongDesc", "ShortDesc", "SkipTimes", "StartDate", "UserID" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000003"), 21000m, (short)23, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 14000m, (short)28, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 15000m, (short)13, new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2021, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "ID", "Amount", "DueDate", "Status", "TrackBillID", "UserID" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 2100m, new DateTime(2021, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 2100m, new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 14000m, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 15000m, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active", new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_SchedulerEntry",
                columns: new[] { "ID", "Amount", "DueDate", "GeneratedUserBillID", "IsGenerated", "Remarks", "TrackBillSchedulerID" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 21000m, new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002"), true, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 21000m, new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 21000m, new DateTime(2021, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 21000m, new DateTime(2021, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 14000m, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000003"), true, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 14000m, new DateTime(2021, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 14000m, new DateTime(2021, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 14000m, new DateTime(2021, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), 15000m, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000004"), true, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), 15000m, new DateTime(2021, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), 15000m, new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), 15000m, new DateTime(2021, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("00000000-0000-0000-0000-000000000005") }
                });

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
                name: "IX_tbl_ProviderTypeConfigScheduler_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler",
                columns: new[] { "ID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigScheduler_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler",
                column: "UserID");

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
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigEmailID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigEmailID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigSchedulerID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigSchedulerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackBill_N_ProviderTypeConfigWebServiceID",
                schema: "dbo",
                table: "tbl_TrackBill",
                column: "N_ProviderTypeConfigWebServiceID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_TrackBill_tbl_Bill_BillID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_TrackBill_tbl_ProviderType_ProviderTypeID",
                schema: "dbo",
                table: "tbl_TrackBill");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ProviderTypeConfigScheduler_tbl_TrackBill_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler");

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
                name: "tbl_UserBillPayment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Bill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_TrackBill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigEmail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigScheduler",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigWebService",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_User",
                schema: "dbo");
        }
    }
}
