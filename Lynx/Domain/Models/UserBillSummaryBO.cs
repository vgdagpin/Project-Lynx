using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class UserBillSummaryBO
    {
        public Guid ID { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public BillPaymentStatus Status { get; set; }
     

        public static IEnumerable<UserBillSummaryBO> Empty()
        {
            return new List<UserBillSummaryBO>();
        }

        public static UserBillSummaryBO Null()
        {
            return null;
        }
    }
}
