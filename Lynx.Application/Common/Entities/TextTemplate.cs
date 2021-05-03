using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class TextTemplate : BaseEntity
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Version { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
