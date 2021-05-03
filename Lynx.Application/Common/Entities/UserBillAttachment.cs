using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserBillAttachment : BaseEntity
    {
        public Guid ID { get; set; }
        public Guid UserBillID { get; set; }
    }
}
