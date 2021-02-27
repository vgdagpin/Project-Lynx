using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Lynx.WebAPI.Common.Constants
{
    public abstract class JwtConstant
    {
        public const string SecurityAlgo = SecurityAlgorithms.HmacSha256;
    }
}
