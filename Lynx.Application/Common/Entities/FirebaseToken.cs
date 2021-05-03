using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class FirebaseToken : BaseEntity
    {
        public long ID { get; set; }
        public string Token { get; set; }

        public Guid? UserID { get; set; }
    }
}
