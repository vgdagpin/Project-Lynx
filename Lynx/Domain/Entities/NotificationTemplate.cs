using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        public Guid ID { get; set; }
        public string TemplateCode { get; set; }
        public string Content { get; set; }
    }
}