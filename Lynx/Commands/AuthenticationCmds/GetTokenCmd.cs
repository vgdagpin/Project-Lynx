﻿using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.AuthenticationCmds
{
    public class GetTokenCmd : ITasq<UserSessionBO>
    {
        public GetTokenCmd(Guid userID)
        {
            UserID = userID;
        }

        public Guid UserID { get; }
    }
}
