using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Lynx.WebAPI.Common.Extensions
{
    public static class IWebHostEnvironmentExtensions
    {
        public static bool IsDevelopment(this IWebHostEnvironment environment)
        {
            return true;
        }

        public static bool IsStaging(this IWebHostEnvironment environment)
        {
            return false;
        }
    }
}
