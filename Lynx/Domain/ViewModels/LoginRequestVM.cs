using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lynx.Domain.ViewModels
{
    public class LoginRequestVM
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string FirebaseToken { get; set; }
    }
}
