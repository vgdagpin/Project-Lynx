using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.ViewModels
{
    public class BillPaymentStepsTemplateSummaryVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
    }

    public class BillPaymentStepsTemplateVM : BillPaymentStepsTemplateSummaryVM
    {
        public string LongDesc { get; set; }
        public string Keywords { get; set; }
    }
}
