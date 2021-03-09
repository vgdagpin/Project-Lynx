using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class Email : BaseEntity
    {
        public long ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool? IsProcessed { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public string Remarks { get; set; }

        public EmailBody N_Body { get; set; }

        public ICollection<EmailPart> N_Headers { get; set; } = new HashSet<EmailPart>();
        public ICollection<EmailAttachment> N_Attachments { get; set; } = new HashSet<EmailAttachment>();
    }
}
