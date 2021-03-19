using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.ViewModels
{
    public class TextTemplateVM
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
