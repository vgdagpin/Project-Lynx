using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeConfigSchedulerVM : IMapFrom<ProviderTypeConfigScheduler>
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

        public IEnumerable<SchedulerEntryVM> SchedulerEntries { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProviderTypeConfigScheduler, ProviderTypeConfigSchedulerVM>()
                .ForMember(t => t.SchedulerEntries, s => s.MapFrom(sprop => sprop.N_ScheduleEntries));
        }
    }
}
