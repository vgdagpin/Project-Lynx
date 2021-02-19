using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Common.ViewModels
{
    public class BillVM : Bill, IMapFrom<Bill>
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }
    }
}
