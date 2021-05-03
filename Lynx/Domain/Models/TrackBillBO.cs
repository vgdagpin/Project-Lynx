using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class TrackBillBO
    {
        public Guid ID { get; set; }


        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string AccountNumber { get; set; }
        public bool IsEnabled { get; set; }
        public BillBO Bill { get; set; }
        public BillProviderBO BillProvider { get; set; }

        public ProviderTypeConfigEmailBO ProviderTypeConfigEmail { get; set; }
        public ProviderTypeConfigSchedulerBO ProviderTypeConfigScheduler { get; set; }
        public ProviderTypeConfigWebServiceBO ProviderTypeConfigWebService { get; set; }

        public static TrackBillBO Null()
        {
            return null;
        }
    }
}
