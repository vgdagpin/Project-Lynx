using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Lynx.WebAPI.Common.Extensions;

namespace Lynx.WebAPI.Middlewares
{
    //https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var _exception = ex.InnermostException();
            Type _exceptionType = _exception.GetType();

            string _errorType = _exceptionType.Name;
            string _message = _exception.Message;

            var _result = JsonConvert.SerializeObject(new
            {
                type = _errorType,
                message = _message
            });

            if (env.IsDevelopment() || env.IsStaging())
            {
                _result = JsonConvert.SerializeObject(new
                {
                    type = _exceptionType.Name,
                    message = _exception.Message,
                    stactTrace = _exception.StackTrace
                });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(_result);
        }
    }
}
