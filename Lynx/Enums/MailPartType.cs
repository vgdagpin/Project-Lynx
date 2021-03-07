using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public enum MailPartType : byte
    {
        None = 0 ,
        Header = 1,
        Form = 2,
        Attachment = 3
    }
}
