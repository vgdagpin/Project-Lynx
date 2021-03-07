using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum TokenStatus : byte
    {
        None = 0,
        Active = 1,
        Expired = 2,
        Invalid = 3
    }
}
