using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class NotificationConfiguration : BaseEntity
    {
        public Guid UserBillTrackingID { get; set; }

        public bool IsScheduled { get; set; }

        public bool IsEnabled { get; set; }

        public TrackBill N_UserBillTracking { get; set; }
    }
}
