using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class User
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserLogin UserLogin { get; set; }
    }
}
