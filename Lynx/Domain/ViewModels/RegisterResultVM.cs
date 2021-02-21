using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Common.ViewModels
{
    public class RegisterResultVM
    {
        public bool IsSuccess { get; set; }
        public Exception Error { get; set; }
        public User User { get; set; }

        public static RegisterResultVM Failed(string reason)
        {
            return new RegisterResultVM
            {
                IsSuccess = false,
                Error = new Exception(reason)
            };
        }

        public static RegisterResultVM Failed(Exception error)
        {
            return new RegisterResultVM
            {
                IsSuccess = false,
                Error = error
            };
        }

        public static RegisterResultVM Success()
        {
            return new RegisterResultVM
            {
                IsSuccess = true
            };
        }

        public static RegisterResultVM Ready(User user)
        {
            return new RegisterResultVM
            {
                IsSuccess = true,
                User = user
            };
        }
    }
}
