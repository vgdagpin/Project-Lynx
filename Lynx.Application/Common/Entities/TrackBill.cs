using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class TrackBill : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public short BillID { get; set; }
        public short ProviderTypeID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public string AccountNumber { get; set; }


        public bool IsEnabled { get; set; } = true;


        public User N_User { get; set; }
        public Bill N_Bill { get; set; }
        public ProviderType N_ProviderType { get; set; }

        public ProviderTypeConfigEmail N_ProviderTypeConfigEmail { get; set; }
        public ProviderTypeConfigScheduler N_ProviderTypeConfigScheduler { get; set; }
        public ProviderTypeConfigWebService N_ProviderTypeConfigWebService { get; set; }

        public ICollection<NotificationConfiguration> N_NotificationConfigurations { get; private set; } = new HashSet<NotificationConfiguration>();
        public ICollection<TrackBillSetting> N_TrackBillSettings { get; set; } = new HashSet<TrackBillSetting>();
        public ICollection<UserBill> N_UserBills { get; private set; } = new HashSet<UserBill>();
    }
}