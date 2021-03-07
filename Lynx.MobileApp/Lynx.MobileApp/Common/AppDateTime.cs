using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Common
{
    public class AppDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
