using System;
using Lynx.Interfaces;

namespace Lynx.WebAPI.Common
{
    public class AppDateTime : IDateTime
    {
        public DateTime Now { get { return DateTime.UtcNow; } }
    }
}
