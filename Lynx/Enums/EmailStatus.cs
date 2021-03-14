using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum  EmailStatus : byte
    {
        Ready = 1,
        Extracted = 2,
        ExtractionError = 3,
        Processed = 4,
        Invalid = 10
    }
}
