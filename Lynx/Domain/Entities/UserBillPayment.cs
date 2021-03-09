using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserBillPayment : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserBillID { get; set; }

        public decimal Amount { get; set; }
        public DateTime? PaidOn { get; set; }

        public ICollection<UserBillPaymentTransaction> N_UserBillPaymentTransactions { get; set; } = new HashSet<UserBillPaymentTransaction>();
    }
}
