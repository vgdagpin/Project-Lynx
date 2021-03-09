using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Lynx
{
    public static class ILoggerExtensions
    {
        public static void LogError(this ILogger logger, Exception exception)
        {
            var ex = exception.InnermostException();

            logger.LogError(ex, ex.Message);
        }
    }
}
