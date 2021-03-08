using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Interfaces
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string data);
        string Serialize<T>(T data);
    }
}
