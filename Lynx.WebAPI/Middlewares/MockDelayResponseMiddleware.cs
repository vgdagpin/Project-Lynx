using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lynx.WebAPI.Middlewares
{
    public class MockDelayResponseMiddleware
    {
        private readonly RequestDelegate next;

        public MockDelayResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Thread.Sleep(2000);

            await next(context);
        }
    }
}
