using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public bool IsEnabled { get; set; } = true;

        public ICollection<BillSetting> N_BillSettings { get; private set; } = new HashSet<BillSetting>();
        public ICollection<BillProvider> N_BillProviders { get; private set; } = new HashSet<BillProvider>();
        public ICollection<BillPaymentStepsTemplate> N_PaymentStepsTemplates { get; private set; } = new HashSet<BillPaymentStepsTemplate>();
    }
}