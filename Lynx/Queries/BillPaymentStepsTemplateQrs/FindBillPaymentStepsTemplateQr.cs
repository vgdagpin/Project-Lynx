using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.BillPaymentStepsTemplateQrs
{
    public class FindBillPaymentStepsTemplateQr : ITasq<BillPaymentStepsTemplateVM>
    {
        public FindBillPaymentStepsTemplateQr(int billPaymentStepsTemplateID)
        {
            BillPaymentStepsTemplateID = billPaymentStepsTemplateID;
        }

        public int BillPaymentStepsTemplateID { get; }
    }
}
