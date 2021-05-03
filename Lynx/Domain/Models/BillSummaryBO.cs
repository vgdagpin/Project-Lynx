using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class BillSummaryBO
    {
        public short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public IEnumerable<BillProviderBO> Providers { get; set; }

        public static IEnumerable<BillSummaryBO> Empty()
        {
            return new List<BillSummaryBO>();
        }

        public static BillSummaryBO Null()
        {
            return null;
        }

        public override string ToString()
        {
            return ShortDesc ?? LongDesc;
        }
    }
}
