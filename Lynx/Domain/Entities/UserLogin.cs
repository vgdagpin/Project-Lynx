using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class UserLogin : BaseEntity
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Password { get; set; }
        public bool IsTemporaryPassword { get; set; }
        public string TemporaryPassword { get; set; }

        public User User { get; set; }
    }
}
