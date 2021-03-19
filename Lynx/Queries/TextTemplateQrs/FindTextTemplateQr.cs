using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.TextTemplateQrs
{
    public class FindTextTemplateQr : ITasq<TextTemplateVM>
    {
        public FindTextTemplateQr(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return;
            }

            if (code.Trim().Length < 3)
            {
                return;
            }

            Code = code.Trim();
        }

        public string Code { get; }
    }
}
