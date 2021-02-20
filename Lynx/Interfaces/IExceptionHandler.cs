using System;

namespace Lynx
{
    public interface IExceptionHandler
    {
        void LogError(Exception exception);
    }
}
