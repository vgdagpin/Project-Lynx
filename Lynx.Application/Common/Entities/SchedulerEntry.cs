using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class SchedulerEntry : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid TrackBillSchedulerID { get; set; }

        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }

        public bool? IsGenerated { get; set; }
        public Guid? GeneratedUserBillID { get; set; }

        public string Remarks { get; set; }
    }
}