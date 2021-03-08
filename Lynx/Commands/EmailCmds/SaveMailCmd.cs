using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.EmailCmds
{
    public class SaveMailCmd : ITasq<long>
    {
        public SaveMailCmd(IEnumerable<MailPart> data)
        {
            Data = data;
        }
        public IEnumerable<MailPart> Data { get; }
    }
}
