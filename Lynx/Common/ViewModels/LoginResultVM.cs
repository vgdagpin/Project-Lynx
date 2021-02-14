using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Common.ViewModels
{
    public class LoginResultVM
    {
        public bool IsSuccess { get; set; }
        public int LoginAttemptCount { get; set; }

        public Exception Error { get; set; }
    }
}
