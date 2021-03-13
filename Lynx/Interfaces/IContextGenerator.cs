﻿using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Lynx.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Interfaces
{
	public interface ILynxDbContext
	{
		#region Entities
		IQueryable<Bill> Bills { get; }
		IQueryable<BillProvider> BillProviders { get; }
		IQueryable<BillSetting> BillSettings { get; }
		IQueryable<Email> Emails { get; }
		IQueryable<EmailAttachment> EmailAttachments { get; }
		IQueryable<EmailBody> EmailBodies { get; }
		IQueryable<EmailPart> EmailParts { get; }
		IQueryable<EmailWorker> EmailWorkers { get; }
		IQueryable<NotificationConfiguration> NotificationConfigurations { get; }
		IQueryable<NotificationTemplate> NotificationTemplates { get; }
		IQueryable<ProviderType> ProviderTypes { get; }
		IQueryable<ProviderTypeConfigEmail> ProviderTypeConfigEmails { get; }
		IQueryable<ProviderTypeConfigScheduler> ProviderTypeConfigSchedulers { get; }
		IQueryable<ProviderTypeConfigWebService> ProviderTypeConfigWebServices { get; }
		IQueryable<SchedulerEntry> SchedulerEntries { get; }
		IQueryable<TrackBill> TrackBills { get; }
		IQueryable<TrackBillSetting> TrackBillSettings { get; }
		IQueryable<User> Users { get; }
		IQueryable<UserBill> UserBills { get; }
		IQueryable<UserBillAttachment> UserBillAttachments { get; }
		IQueryable<UserBillPayment> UserBillPayments { get; }
		IQueryable<UserBillPaymentTransaction> UserBillPaymentTransactions { get; }
		IQueryable<UserLogin> UserLogins { get; }
		IQueryable<UserNotification> UserNotifications { get; }
		IQueryable<UserSession> UserSessions { get; }
        #endregion
	}
}

