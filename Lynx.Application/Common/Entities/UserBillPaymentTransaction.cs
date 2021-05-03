using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserBillPaymentTransaction : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserBillPaymentID { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Remarks { get; set; }
    }
}
