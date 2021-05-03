using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Lynx.Interfaces;
using Lynx.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Infrastructure.Persistence
{
	public partial class LynxDbContext : DbContext, ILynxDbContext
	{
		public Guid UID { get; } = Guid.NewGuid();
		public bool HasSeedData { get; set; }

		#region Entities
		private DbSet<Bill> db_Bills { get; set; }
		public IQueryable<Bill> Bills 
		{ 
			get { return db_Bills; }
			private set { db_Bills = (DbSet<Bill>)value; }
		}
		private DbSet<BillPaymentStepsTemplate> db_BillPaymentStepsTemplates { get; set; }
		public IQueryable<BillPaymentStepsTemplate> BillPaymentStepsTemplates 
		{ 
			get { return db_BillPaymentStepsTemplates; }
			private set { db_BillPaymentStepsTemplates = (DbSet<BillPaymentStepsTemplate>)value; }
		}
		private DbSet<BillProvider> db_BillProviders { get; set; }
		public IQueryable<BillProvider> BillProviders 
		{ 
			get { return db_BillProviders; }
			private set { db_BillProviders = (DbSet<BillProvider>)value; }
		}
		private DbSet<BillSetting> db_BillSettings { get; set; }
		public IQueryable<BillSetting> BillSettings 
		{ 
			get { return db_BillSettings; }
			private set { db_BillSettings = (DbSet<BillSetting>)value; }
		}
		private DbSet<Email> db_Emails { get; set; }
		public IQueryable<Email> Emails 
		{ 
			get { return db_Emails; }
			private set { db_Emails = (DbSet<Email>)value; }
		}
		private DbSet<EmailAttachment> db_EmailAttachments { get; set; }
		public IQueryable<EmailAttachment> EmailAttachments 
		{ 
			get { return db_EmailAttachments; }
			private set { db_EmailAttachments = (DbSet<EmailAttachment>)value; }
		}
		private DbSet<EmailBody> db_EmailBodies { get; set; }
		public IQueryable<EmailBody> EmailBodies 
		{ 
			get { return db_EmailBodies; }
			private set { db_EmailBodies = (DbSet<EmailBody>)value; }
		}
		private DbSet<EmailExtract> db_EmailExtracts { get; set; }
		public IQueryable<EmailExtract> EmailExtracts 
		{ 
			get { return db_EmailExtracts; }
			private set { db_EmailExtracts = (DbSet<EmailExtract>)value; }
		}
		private DbSet<EmailPart> db_EmailParts { get; set; }
		public IQueryable<EmailPart> EmailParts 
		{ 
			get { return db_EmailParts; }
			private set { db_EmailParts = (DbSet<EmailPart>)value; }
		}
		private DbSet<EmailWorker> db_EmailWorkers { get; set; }
		public IQueryable<EmailWorker> EmailWorkers 
		{ 
			get { return db_EmailWorkers; }
			private set { db_EmailWorkers = (DbSet<EmailWorker>)value; }
		}
		private DbSet<FirebaseToken> db_FirebaseTokens { get; set; }
		public IQueryable<FirebaseToken> FirebaseTokens 
		{ 
			get { return db_FirebaseTokens; }
			private set { db_FirebaseTokens = (DbSet<FirebaseToken>)value; }
		}
		private DbSet<NotificationConfiguration> db_NotificationConfigurations { get; set; }
		public IQueryable<NotificationConfiguration> NotificationConfigurations 
		{ 
			get { return db_NotificationConfigurations; }
			private set { db_NotificationConfigurations = (DbSet<NotificationConfiguration>)value; }
		}
		private DbSet<NotificationTemplate> db_NotificationTemplates { get; set; }
		public IQueryable<NotificationTemplate> NotificationTemplates 
		{ 
			get { return db_NotificationTemplates; }
			private set { db_NotificationTemplates = (DbSet<NotificationTemplate>)value; }
		}
		private DbSet<ProviderType> db_ProviderTypes { get; set; }
		public IQueryable<ProviderType> ProviderTypes 
		{ 
			get { return db_ProviderTypes; }
			private set { db_ProviderTypes = (DbSet<ProviderType>)value; }
		}
		private DbSet<ProviderTypeConfigEmail> db_ProviderTypeConfigEmails { get; set; }
		public IQueryable<ProviderTypeConfigEmail> ProviderTypeConfigEmails 
		{ 
			get { return db_ProviderTypeConfigEmails; }
			private set { db_ProviderTypeConfigEmails = (DbSet<ProviderTypeConfigEmail>)value; }
		}
		private DbSet<ProviderTypeConfigScheduler> db_ProviderTypeConfigSchedulers { get; set; }
		public IQueryable<ProviderTypeConfigScheduler> ProviderTypeConfigSchedulers 
		{ 
			get { return db_ProviderTypeConfigSchedulers; }
			private set { db_ProviderTypeConfigSchedulers = (DbSet<ProviderTypeConfigScheduler>)value; }
		}
		private DbSet<ProviderTypeConfigWebService> db_ProviderTypeConfigWebServices { get; set; }
		public IQueryable<ProviderTypeConfigWebService> ProviderTypeConfigWebServices 
		{ 
			get { return db_ProviderTypeConfigWebServices; }
			private set { db_ProviderTypeConfigWebServices = (DbSet<ProviderTypeConfigWebService>)value; }
		}
		private DbSet<SchedulerEntry> db_SchedulerEntries { get; set; }
		public IQueryable<SchedulerEntry> SchedulerEntries 
		{ 
			get { return db_SchedulerEntries; }
			private set { db_SchedulerEntries = (DbSet<SchedulerEntry>)value; }
		}
		private DbSet<TextTemplate> db_TextTemplates { get; set; }
		public IQueryable<TextTemplate> TextTemplates 
		{ 
			get { return db_TextTemplates; }
			private set { db_TextTemplates = (DbSet<TextTemplate>)value; }
		}
		private DbSet<TrackBill> db_TrackBills { get; set; }
		public IQueryable<TrackBill> TrackBills 
		{ 
			get { return db_TrackBills; }
			private set { db_TrackBills = (DbSet<TrackBill>)value; }
		}
		private DbSet<TrackBillSetting> db_TrackBillSettings { get; set; }
		public IQueryable<TrackBillSetting> TrackBillSettings 
		{ 
			get { return db_TrackBillSettings; }
			private set { db_TrackBillSettings = (DbSet<TrackBillSetting>)value; }
		}
		private DbSet<User> db_Users { get; set; }
		public IQueryable<User> Users 
		{ 
			get { return db_Users; }
			private set { db_Users = (DbSet<User>)value; }
		}
		private DbSet<UserBill> db_UserBills { get; set; }
		public IQueryable<UserBill> UserBills 
		{ 
			get { return db_UserBills; }
			private set { db_UserBills = (DbSet<UserBill>)value; }
		}
		private DbSet<UserBillAttachment> db_UserBillAttachments { get; set; }
		public IQueryable<UserBillAttachment> UserBillAttachments 
		{ 
			get { return db_UserBillAttachments; }
			private set { db_UserBillAttachments = (DbSet<UserBillAttachment>)value; }
		}
		private DbSet<UserBillPayment> db_UserBillPayments { get; set; }
		public IQueryable<UserBillPayment> UserBillPayments 
		{ 
			get { return db_UserBillPayments; }
			private set { db_UserBillPayments = (DbSet<UserBillPayment>)value; }
		}
		private DbSet<UserBillPaymentTransaction> db_UserBillPaymentTransactions { get; set; }
		public IQueryable<UserBillPaymentTransaction> UserBillPaymentTransactions 
		{ 
			get { return db_UserBillPaymentTransactions; }
			private set { db_UserBillPaymentTransactions = (DbSet<UserBillPaymentTransaction>)value; }
		}
		private DbSet<UserLogin> db_UserLogins { get; set; }
		public IQueryable<UserLogin> UserLogins 
		{ 
			get { return db_UserLogins; }
			private set { db_UserLogins = (DbSet<UserLogin>)value; }
		}
		private DbSet<UserNotification> db_UserNotifications { get; set; }
		public IQueryable<UserNotification> UserNotifications 
		{ 
			get { return db_UserNotifications; }
			private set { db_UserNotifications = (DbSet<UserNotification>)value; }
		}
		private DbSet<UserSession> db_UserSessions { get; set; }
		public IQueryable<UserSession> UserSessions 
		{ 
			get { return db_UserSessions; }
			private set { db_UserSessions = (DbSet<UserSession>)value; }
		}
        #endregion

		public LynxDbContext(DbContextOptions<LynxDbContext> dbContextOpt) 
			: base(dbContextOpt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
			=> modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}

namespace Lynx.Infrastructure.Persistence.Configurations
{
	#region Configurations
	public partial class Bill_Configuration : BaseConfiguration<Bill> { }
	public partial class BillPaymentStepsTemplate_Configuration : BaseConfiguration<BillPaymentStepsTemplate> { }
	public partial class BillProvider_Configuration : BaseConfiguration<BillProvider> { }
	public partial class BillSetting_Configuration : BaseConfiguration<BillSetting> { }
	public partial class Email_Configuration : BaseConfiguration<Email> { }
	public partial class EmailAttachment_Configuration : BaseConfiguration<EmailAttachment> { }
	public partial class EmailBody_Configuration : BaseConfiguration<EmailBody> { }
	public partial class EmailExtract_Configuration : BaseConfiguration<EmailExtract> { }
	public partial class EmailPart_Configuration : BaseConfiguration<EmailPart> { }
	public partial class EmailWorker_Configuration : BaseConfiguration<EmailWorker> { }
	public partial class FirebaseToken_Configuration : BaseConfiguration<FirebaseToken> { }
	public partial class NotificationConfiguration_Configuration : BaseConfiguration<NotificationConfiguration> { }
	public partial class NotificationTemplate_Configuration : BaseConfiguration<NotificationTemplate> { }
	public partial class ProviderType_Configuration : BaseConfiguration<ProviderType> { }
	public partial class ProviderTypeConfigEmail_Configuration : BaseConfiguration<ProviderTypeConfigEmail> { }
	public partial class ProviderTypeConfigScheduler_Configuration : BaseConfiguration<ProviderTypeConfigScheduler> { }
	public partial class ProviderTypeConfigWebService_Configuration : BaseConfiguration<ProviderTypeConfigWebService> { }
	public partial class SchedulerEntry_Configuration : BaseConfiguration<SchedulerEntry> { }
	public partial class TextTemplate_Configuration : BaseConfiguration<TextTemplate> { }
	public partial class TrackBill_Configuration : BaseConfiguration<TrackBill> { }
	public partial class TrackBillSetting_Configuration : BaseConfiguration<TrackBillSetting> { }
	public partial class User_Configuration : BaseConfiguration<User> { }
	public partial class UserBill_Configuration : BaseConfiguration<UserBill> { }
	public partial class UserBillAttachment_Configuration : BaseConfiguration<UserBillAttachment> { }
	public partial class UserBillPayment_Configuration : BaseConfiguration<UserBillPayment> { }
	public partial class UserBillPaymentTransaction_Configuration : BaseConfiguration<UserBillPaymentTransaction> { }
	public partial class UserLogin_Configuration : BaseConfiguration<UserLogin> { }
	public partial class UserNotification_Configuration : BaseConfiguration<UserNotification> { }
	public partial class UserSession_Configuration : BaseConfiguration<UserSession> { }
    #endregion
}
