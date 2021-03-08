using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailBody : BaseEntity
    {
        public new long ID { get; set; }

        public string Content { get; set; }

        public Email N_Email { get; set; }
    }
}
