using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.ViewModels
{
    public class MailPart
    {
        public MailPartType PartType { get; set; }
        public string Key { get; set; }
        public Dictionary<string, object> Value { get; set; }
    }
}
