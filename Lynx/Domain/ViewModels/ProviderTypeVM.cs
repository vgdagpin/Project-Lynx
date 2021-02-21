using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.Domain.ViewModels
{
    public class ProviderTypeVM : IMapFrom<ProviderType>
    {
        public short ID { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
}
