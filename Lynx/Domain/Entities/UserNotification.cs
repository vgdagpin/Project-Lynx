using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserNotification : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }

        public string Content { get; set; }
        public bool IsOpened { get; set; }
        public DateTime ReceivedOn { get; set; }

        public User N_User { get; set; }
    }
}
