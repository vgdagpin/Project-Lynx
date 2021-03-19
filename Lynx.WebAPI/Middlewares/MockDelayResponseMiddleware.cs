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
        public const int DelayInMilliseconds = 2000;

        private readonly RequestDelegate next;

        public MockDelayResponseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
#if DEBUG
            // this helps the development of responsiveness of the UI
            Thread.Sleep(DelayInMilliseconds);
#endif           
            await next(context);
        }
    }
}