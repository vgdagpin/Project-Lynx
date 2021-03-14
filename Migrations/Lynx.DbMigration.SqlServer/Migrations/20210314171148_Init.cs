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
                    ID = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    ShortDesc = table.Column<string>(maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bill", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Email",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(maxLength: 500, nullable: true),
                    To = table.Column<string>(maxLength: 500, nullable: true),
                    CC = table.Column<string>(maxLength: 500, nullable: true),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    ExtractedOn = table.Column<DateTime>(nullable: true),
                    ProcessedOn = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Email", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NotificationTemplate",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TemplateCode = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
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
                    ID = table.Column<short>(nullable: false),
                    ShortDesc = table.Column<string>(maxLength: 50, nullable: false),
                    LongDesc = table.Column<string>(maxLength: 100, nullable: false)
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
                    ID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
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
                    BillID = table.Column<short>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 500, nullable: false)
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
                name: "tbl_EmailAttachment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(nullable: false),
                    ContentType = table.Column<string>(maxLength: 100, nullable: false),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    Length = table.Column<long>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailAttachment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailAttachment_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmailBody",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    Html = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Raw = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailBody", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailBody_tbl_Email_ID",
                        column: x => x.ID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmailPart",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailID = table.Column<long>(nullable: false),
                    PartType = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "tbl_BillProvider",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<short>(nullable: false),
                    ProviderTypeID = table.Column<short>(nullable: false),
                    ShortDesc = table.Column<string>(maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(maxLength: 100, nullable: true)
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
                name: "tbl_FirebaseToken",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(maxLength: 300, nullable: false),
                    UserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FirebaseToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_FirebaseToken_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackBill",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    BillID = table.Column<short>(nullable: false),
                    ProviderTypeID = table.Column<short>(nullable: false),
                    ShortDesc = table.Column<string>(maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
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
                    ID = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    Salt = table.Column<byte[]>(nullable: true),
                    Password = table.Column<byte[]>(nullable: true),
                    IsTemporaryPassword = table.Column<bool>(nullable: false),
                    TemporaryPassword = table.Column<string>(maxLength: 100, nullable: false)
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
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    IsSent = table.Column<bool>(nullable: true),
                    ProcessedOn = table.Column<DateTime>(nullable: true),
                    OpenedOn = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
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
                    SessionID = table.Column<Guid>(nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ExpiredOn = table.Column<DateTime>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
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
                name: "tbl_EmailWorker",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<short>(nullable: false),
                    AssemblyName = table.Column<string>(maxLength: 200, nullable: false),
                    TypeName = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailWorker", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_EmailWorker_tbl_BillProvider_ID",
                        column: x => x.ID,
                        principalSchema: "dbo",
                        principalTable: "tbl_BillProvider",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NotificationConfiguration",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserBillTrackingID = table.Column<Guid>(nullable: false),
                    IsScheduled = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    N_UserBillTrackingID = table.Column<Guid>(nullable: true),
                    N_UserBillTrackingUserID = table.Column<Guid>(nullable: true)
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
                name: "tbl_ProviderTypeConfigEmail",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ClientEmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    ReceiverEmailAddress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigEmail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigEmail_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigEmail_tbl_TrackBill_ID_UserID",
                        columns: x => new { x.ID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProviderTypeConfigScheduler",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ShortDesc = table.Column<string>(maxLength: 50, nullable: true),
                    LongDesc = table.Column<string>(maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Frequency = table.Column<string>(maxLength: 20, nullable: true),
                    DayFrequency = table.Column<short>(nullable: true),
                    SkipTimes = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigScheduler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigScheduler_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigScheduler_tbl_TrackBill_ID_UserID",
                        columns: x => new { x.ID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ProviderTypeConfigWebService",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Indentity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProviderTypeConfigWebService", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigWebService_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tbl_ProviderTypeConfigWebService_tbl_TrackBill_ID_UserID",
                        columns: x => new { x.ID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackBillSetting",
                schema: "dbo",
                columns: table => new
                {
                    TrackBillID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    ID = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(maxLength: 500, nullable: false)
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
                    ID = table.Column<Guid>(nullable: false),
                    TrackBillID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    Status = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UserBill_tbl_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "tbl_User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tbl_UserBill_tbl_TrackBill_TrackBillID_UserID",
                        columns: x => new { x.TrackBillID, x.UserID },
                        principalSchema: "dbo",
                        principalTable: "tbl_TrackBill",
                        principalColumns: new[] { "ID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmailExtract",
                schema: "dbo",
                columns: table => new
                {
                    EmailID = table.Column<long>(nullable: false),
                    EmailWorkerID = table.Column<short>(nullable: false),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmailExtract", x => new { x.EmailID, x.EmailWorkerID, x.Key });
                    table.ForeignKey(
                        name: "FK_tbl_EmailExtract_tbl_Email_EmailID",
                        column: x => x.EmailID,
                        principalSchema: "dbo",
                        principalTable: "tbl_Email",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EmailExtract_tbl_EmailWorker_EmailWorkerID",
                        column: x => x.EmailWorkerID,
                        principalSchema: "dbo",
                        principalTable: "tbl_EmailWorker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SchedulerEntry",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TrackBillSchedulerID = table.Column<Guid>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    IsGenerated = table.Column<bool>(nullable: true),
                    GeneratedUserBillID = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true)
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
                    ID = table.Column<Guid>(nullable: false),
                    UserBillID = table.Column<Guid>(nullable: false)
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
                    ID = table.Column<Guid>(nullable: false),
                    UserBillID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(20,6)", nullable: false),
                    PaidOn = table.Column<DateTime>(nullable: true)
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
                    ID = table.Column<Guid>(nullable: false),
                    UserBillPaymentID = table.Column<Guid>(nullable: false),
                    TransactionStatus = table.Column<byte>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
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
                columns: new[] { "ID", "Code", "IsEnabled", "LongDesc", "ShortDesc" },
                values: new object[,]
                {
                    { (short)1, "Globe", true, "Globe", "Globe" },
                    { (short)2, "Meralco", true, "Meralco", "Meralco" },
                    { (short)3, "Home Loan Amortization", true, "Home Loan Amortization", "Home Loan Amortization" },
                    { (short)4, "Car Loan Amortization", true, "Car Loan Amortization", "Car Loan Amortization" },
                    { (short)5, "Credit Card", true, "Credit Card", "Credit Card" }
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
                    { (short)2, (short)2, null, (short)3, null },
                    { (short)5, (short)5, "BDO", (short)3, "BDO" },
                    { (short)6, (short)5, "Metrobank", (short)3, "Metrobank" }
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
                table: "tbl_EmailWorker",
                columns: new[] { "ID", "AssemblyName", "TypeName" },
                values: new object[] { (short)1, "Lynx.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Lynx.Application.Handlers.Commands.EmailWorkerCmds.ReadUserBillFromGlobeEmailCmdHandler" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_EmailWorker",
                columns: new[] { "ID", "AssemblyName", "TypeName" },
                values: new object[] { (short)5, "Lynx.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Lynx.Application.Handlers.Commands.EmailWorkerCmds.ReadUserBillFromBDOCmdHandler" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_EmailWorker",
                columns: new[] { "ID", "AssemblyName", "TypeName" },
                values: new object[] { (short)6, "Lynx.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Lynx.Application.Handlers.Commands.EmailWorkerCmds.ReadUserBillFromMetrobankCmdHandler" });

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
                name: "IX_tbl_EmailAttachment_EmailID",
                schema: "dbo",
                table: "tbl_EmailAttachment",
                column: "EmailID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailExtract_EmailWorkerID",
                schema: "dbo",
                table: "tbl_EmailExtract",
                column: "EmailWorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmailPart_EmailID",
                schema: "dbo",
                table: "tbl_EmailPart",
                column: "EmailID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FirebaseToken_Token",
                schema: "dbo",
                table: "tbl_FirebaseToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FirebaseToken_UserID",
                schema: "dbo",
                table: "tbl_FirebaseToken",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_NotificationConfiguration_N_UserBillTrackingID_N_UserBillTrackingUserID",
                schema: "dbo",
                table: "tbl_NotificationConfiguration",
                columns: new[] { "N_UserBillTrackingID", "N_UserBillTrackingUserID" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigEmail_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigEmail",
                columns: new[] { "ID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigScheduler_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigScheduler_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigScheduler",
                columns: new[] { "ID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProviderTypeConfigWebService_ID_UserID",
                schema: "dbo",
                table: "tbl_ProviderTypeConfigWebService",
                columns: new[] { "ID", "UserID" },
                unique: true);

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
                name: "IX_tbl_UserBill_UserID",
                schema: "dbo",
                table: "tbl_UserBill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UserBill_TrackBillID_UserID",
                schema: "dbo",
                table: "tbl_UserBill",
                columns: new[] { "TrackBillID", "UserID" });

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
                name: "tbl_BillSetting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailAttachment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailBody",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailExtract",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_EmailPart",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_FirebaseToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_NotificationConfiguration",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_NotificationTemplate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigEmail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigWebService",
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
                name: "tbl_EmailWorker",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Email",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_ProviderTypeConfigScheduler",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_UserBillPayment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_BillProvider",
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
