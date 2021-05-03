using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailExtract : BaseEntity
    {
        public long EmailID { get; set; }
        public short EmailWorkerID { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }
    }
}