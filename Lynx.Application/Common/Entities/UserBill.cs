using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserBill : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid TrackBillID { get; set; }
        public Guid UserID { get; set; }

        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public BillPaymentStatus Status { get; set; }

        public TrackBill N_TrackBill { get; set; }
        public User N_User { get; set; }
        public ICollection<UserBillPayment> N_UserBillPayments { get; set; } = new HashSet<UserBillPayment>();
        public ICollection<UserBillAttachment> N_Attachments { get; set; } = new HashSet<UserBillAttachment>();
    }
}
