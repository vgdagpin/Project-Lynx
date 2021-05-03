using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserNotification : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public bool? IsSent { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public DateTime? OpenedOn { get; set; }

        public string Remarks { get; set; }

        public User N_User { get; set; }
    }
}
