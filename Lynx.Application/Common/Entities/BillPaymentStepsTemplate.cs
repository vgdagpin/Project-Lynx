using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class BillPaymentStepsTemplate : BaseEntity
    {
        public int ID { get; set; }
        public short BillID { get; set; }

        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Keywords { get; set; }
    }
}
