﻿using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class SchedulerEntryVM : IMapFrom<SchedulerEntry>
    {
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
    }
}