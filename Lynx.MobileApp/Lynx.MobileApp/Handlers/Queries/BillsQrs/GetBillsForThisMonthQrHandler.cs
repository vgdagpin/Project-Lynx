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
            return new List<BillVM>
            {
                new BillVM{ Name = "Globe", DueDate = new DateTime(2021, 2, 24), AmountDue = 1200 },
                new BillVM{ Name = "Meralco", DueDate = new DateTime(2021, 2, 27), AmountDue = 1200 }
            };
        }
    }
}
