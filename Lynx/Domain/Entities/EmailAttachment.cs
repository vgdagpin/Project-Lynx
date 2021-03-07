using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class EmailAttachment : BaseEntity
    {
        public new long ID { get; set; }
        public long EmailID { get; set; }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public byte[] Content { get; set; }
    }
}
