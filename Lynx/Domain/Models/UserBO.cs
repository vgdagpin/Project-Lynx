using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class UserBO
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
