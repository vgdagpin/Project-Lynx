using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lynx.Domain.ViewModels
{
    public class RefreshTokenRequestVM
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
