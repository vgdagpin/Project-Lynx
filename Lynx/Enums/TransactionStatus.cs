using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum TransactionStatus : byte
    {
        Pending = 1,
        Posted = 2,
        Error = 20
    }
}
