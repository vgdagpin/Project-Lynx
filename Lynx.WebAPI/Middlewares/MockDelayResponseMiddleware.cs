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
#if DEBUG
            // this helps the development of responsiveness of the UI
            Thread.Sleep(3000);
#endif           
            await next(context);
        }
    }
}