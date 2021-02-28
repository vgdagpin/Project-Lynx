using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using TasqR;

namespace Lynx.Queries.UserQrs
{
    public class GetUserDetailQr : ITasq<User>
    {
        public GetUserDetailQr(string emailOrUserName)
        {
            EmailOrUserName = emailOrUserName;
        }

        public string EmailOrUserName { get; }
    }
}
