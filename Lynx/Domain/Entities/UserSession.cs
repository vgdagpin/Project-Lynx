using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Enums;

namespace Lynx.Domain.Entities
{
    public class UserSession
    {
        public Guid SessionID { get; set; }
        public Guid UserID { get; set; }

        public string Token { get; set; }
        public SessionStatus Status { get; set; }


        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }

        public string Remarks { get; set; }

        public User User { get; set; }
    }
}
