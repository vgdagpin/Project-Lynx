using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Commands.UserCmds
{
    public class RegisterUserCmd : ITasq<RegisterResultVM>
    {
        public RegisterUserCmd(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
    }
}
