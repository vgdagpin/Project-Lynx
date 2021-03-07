using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.MobileApp.Portable.Common.Enums
{
    public enum SessionVerificationResult : byte
    {
        None = 0,
        NeedLogin = 1,
        Authenticated = 2
    }
}
