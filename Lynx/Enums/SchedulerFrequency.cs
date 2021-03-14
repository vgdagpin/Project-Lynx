using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum SchedulerFrequency : byte
    {
        EverySunday = 1,
        EveryMonday = 2,
        EveryTuesday = 3,
        EveryWednesday = 4,
        EveryThursday = 5, 
        EveryFriday = 6,
        EverySaturday = 7,
        Daily = 10,
        EveryFirstOfTheMonth = 20,
        EveryEndOfTheMonth = 21,
        Monthly = 22
    }
}
