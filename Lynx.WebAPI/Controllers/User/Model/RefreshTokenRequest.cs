using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lynx.WebAPI.Controllers.User.Model
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
