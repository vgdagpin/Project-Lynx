using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lynx.WebAPI.Controllers.User.Model
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
