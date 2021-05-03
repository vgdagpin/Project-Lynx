using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class ProviderTypeConfigScheduler : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal Amount { get; set; }

        public SchedulerFrequency? Frequency { get; set; }

        /// <summary>
        /// Useful for scheduling every 25 of the month
        /// </summary>
        public short? DayFrequency { get; set; }

        /// <summary>
        /// Useful for scheduling every other day
        /// </summary>
        public short? SkipTimes { get; set; }


        public ICollection<SchedulerEntry> N_ScheduleEntries { get; set; } = new HashSet<SchedulerEntry>();
    }
}
