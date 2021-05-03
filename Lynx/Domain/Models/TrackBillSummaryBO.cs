using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class TrackBillSummaryBO
    {
        public Guid ID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string AccountNumber { get; set; }
        public bool IsEnabled { get; set; }
        public ProviderTypeBO ProviderType { get; set; }
    }
}
