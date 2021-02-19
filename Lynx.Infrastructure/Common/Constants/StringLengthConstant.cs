using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Infrastructure
{
    public abstract class StringLengthConstant
    {
        public const int Name = 100;
        public const int Password = 100;
        public const int Token = 500;
        public const int Remarks = 500;

        public const int Code = 50;
        public const int ShortDesc = 50;
        public const int LongDesc = 100;
        public const int AltDesc = 255;

        public const int AssemblyName = 200;
        public const int TypeName = 400;

        public const int Enums = 20;
    }
}
