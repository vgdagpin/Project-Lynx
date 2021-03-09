using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class BillProvider : BaseEntity
    {
        public short ID { get; set; }

        public short BillID { get; set; }
        public short ProviderTypeID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }


        public ProviderType N_ProviderType { get; set; }
    }
}