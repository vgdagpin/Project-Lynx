using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.ViewModels
{
    public class TokenVerificationResult
    {
        public TokenStatus TokenStatus { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
