using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Common
{
    public class AppGuid : IGuid
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}
