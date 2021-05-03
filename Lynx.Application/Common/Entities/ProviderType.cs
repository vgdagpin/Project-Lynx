using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class ProviderType : BaseEntity
    {
        public short ID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
}
