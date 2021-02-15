using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Enums
{
    public enum SessionStatus : byte
    {
        Inactive = 0,
        Active = 1,
        Expired = 2,
        Revoked = 3
    }
}
