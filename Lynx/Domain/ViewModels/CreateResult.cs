using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.ViewModels
{
    public class CreateResult<T> where T : class
    {
        public T NewEntry { get; set; }
        public bool? IsCreated { get; set; }

        public Exception Error { get; set; }
    }
}
