using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class ProviderTypeConfigSchedulerBO
    {
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

        public IEnumerable<SchedulerEntryBO> SchedulerEntries { get; set; }
    }
}
