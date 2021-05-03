using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class ProviderTypeConfigEmail : BaseEntity
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public string ClientEmailAddress { get; set; }
        public string ReceiverEmailAddress { get; set; }
    }
}
