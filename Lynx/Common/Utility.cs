using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lynx
{
    public static class Utility
    {
        public static T Max<T>(params T[] args)
        {
            return args.Max();
        }
    }
}
