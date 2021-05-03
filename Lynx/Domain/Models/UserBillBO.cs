using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class UserBillBO
    {
        public Guid TrackBillID { get; set; }
        public Guid UserID { get; set; }

        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public BillPaymentStatus Status { get; set; }

        public TrackBillBO TrackBill { get; set; }
        public UserBO User { get; set; }




        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public static UserBillBO Null()
        {
            return null;
        }
    }
}
