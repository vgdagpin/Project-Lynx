using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailBody : BaseEntity
    {
        public new long ID { get; set; }

        public string Html { get; set; }
        public string Text { get; set; }
        public string Raw { get; set; }

        public Email N_Email { get; set; }
    }
}
