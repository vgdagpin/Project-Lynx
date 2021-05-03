using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class BillBO
    {
        public short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }


        public bool IsEnabled { get; set; }

        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
    }
}
