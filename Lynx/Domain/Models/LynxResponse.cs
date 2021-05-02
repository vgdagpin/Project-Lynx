using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Lynx.Domain.Models
{
    public class LynxResponse<T>
    {
        public string StringContent { get; set; }
        public T ObjectContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public LynxResponse()
        {
            
        }
    }
}
