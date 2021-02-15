using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Common.ViewModels
{
    public class BillVM
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
    }
}
