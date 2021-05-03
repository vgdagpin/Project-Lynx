using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Models
{
    public class RegisterResultBO
    {
        public bool IsSuccess { get; set; }
        public Exception Error { get; set; }
        public UserBO User { get; set; }

        public static RegisterResultBO Failed(string reason)
        {
            return new RegisterResultBO
            {
                IsSuccess = false,
                Error = new Exception(reason)
            };
        }

        public static RegisterResultBO Failed(Exception error)
        {
            return new RegisterResultBO
            {
                IsSuccess = false,
                Error = error
            };
        }

        public static RegisterResultBO Success()
        {
            return new RegisterResultBO
            {
                IsSuccess = true
            };
        }

        public static RegisterResultBO Ready(UserBO user)
        {
            return new RegisterResultBO
            {
                IsSuccess = true,
                User = user
            };
        }
    }
}
