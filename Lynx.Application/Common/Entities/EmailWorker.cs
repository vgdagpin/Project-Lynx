using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailWorker : BaseEntity
    {
        public short ID { get; set; }

        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
    }
}
