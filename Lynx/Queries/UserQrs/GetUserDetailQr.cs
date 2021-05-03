using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using TasqR;

namespace Lynx.Queries.UserQrs
{
    public class GetUserDetailQr : ITasq<UserBO>
    {
        public GetUserDetailQr(string emailOrUserName)
        {
            EmailOrUserName = emailOrUserName;
        }

        public GetUserDetailQr(Guid id)
        {
            Id = id;
        }

        public string EmailOrUserName { get; }
        public Guid? Id { get; }
    }
}
