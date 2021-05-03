using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class TrackBillSetting
    {
        public Guid ID { get; set; }
        public Guid TrackBillID { get; set; }
        public Guid UserID { get; set; }

        public string Code { get; set; }
        public string Value { get; set; }
    }
}
