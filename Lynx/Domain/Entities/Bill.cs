using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public new short ID { get; set; }

        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }


        public bool IsEnabled { get; set; } = true;

        public string AssemblyName { get; set; }
        public string TypeName { get; set; }

        public ICollection<BillSetting> N_BillSettings { get; set; } = new HashSet<BillSetting>();
    }
}