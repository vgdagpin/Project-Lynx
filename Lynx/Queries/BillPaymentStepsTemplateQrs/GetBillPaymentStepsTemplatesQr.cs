using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.BillPaymentStepsTemplateQrs
{
    public class GetBillPaymentStepsTemplatesQr : ITasq<IEnumerable<BillPaymentStepsTemplateSummaryVM>>
    {
        public GetBillPaymentStepsTemplatesQr(short billID, string query = null)
        {
            BillID = billID;
            Query = query;
        }

        public short BillID { get; }
        public string Query { get; }
    }
}
