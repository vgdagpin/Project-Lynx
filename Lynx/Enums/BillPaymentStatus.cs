using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum BillPaymentStatus : byte
    {
        Pending = 1,
        Active = 2,
        Paid = 3,
        Overdue = 4,
        Error = 20,
        Collection = 30
    }
}
