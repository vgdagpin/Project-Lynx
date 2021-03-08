using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailPart : BaseEntity
    {
        public new long ID { get; set; }

        public long EmailID { get; set; }
        public MailPartType PartType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
