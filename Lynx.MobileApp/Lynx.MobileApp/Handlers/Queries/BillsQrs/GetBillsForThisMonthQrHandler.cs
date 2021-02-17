using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using Lynx.Queries.BillsQrs;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.BillsQrs
{
    public class GetBillsForThisMonthQrHandler : TasqHandler<GetBillsForThisMonthQr, IEnumerable<BillVM>>
    {
        public override IEnumerable<BillVM> Run(GetBillsForThisMonthQr process)
        {
            List<BillVM> retVal = new List<BillVM>();

            for (int i = 0; i < 10; i++)
            {
                retVal.Add(new BillVM { Name = "Globe", DueDate = new DateTime(2021, 2, 24), AmountDue = 1200 });
            }

            return retVal;
        }
    }
}